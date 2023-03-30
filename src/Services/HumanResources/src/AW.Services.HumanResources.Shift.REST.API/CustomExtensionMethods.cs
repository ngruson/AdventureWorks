using AW.Services.Infrastructure.Filters;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AW.SharedKernel.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using AW.SharedKernel.Api;
using AW.Services.SharedKernel.EFCore;
using MediatR;
using AW.Services.SharedKernel.Interfaces;
using AW.Services.HumanResources.Core.Handlers.GetAllEmployees;
using AW.Services.HumanResources.Core.AutoMapper;
using AW.Services.HumanResources.Infrastructure.EFCore.Configurations;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AW.Services.HumanResources.Shift.REST.API
{
    public static class CustomExtensionMethods
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                options.Filters.Add(typeof(ValidateModelStateFilterAttribute));
            });

            return services;
        }

        public static IServiceCollection AddVersioning(this IServiceCollection services)
        {
            services.AddMvcCore()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                .AddApiExplorer();

            services.AddApiVersioning(options => options.ReportApiVersions = true)
                .AddVersionedApiExplorer(
                    options =>
                    {
                        options.GroupNameFormat = "'v'VVV";
                        options.SubstituteApiVersionInUrl = true;
                    }
                );

            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            if (configuration["AuthN:IdP"] == "AzureAd")
            {
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddMicrosoftIdentityWebApi(configuration.GetSection("AuthN:AzureAd"));
            }
            else if (configuration["AuthN:IdP"] == "IdSrv")
            {
                var oidcConfig = new OpenIdConnectConfigurationBuilder(configuration).Build();

                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.Authority = oidcConfig?.Authority;
                        options.Audience = "shift-api";
                        options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                    });
            }

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerDocumentation("Shift API");

            return services;
        }

        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(provider =>
            {
                var builder = new DbContextOptionsBuilder<AWContext>();
                builder.UseSqlServer(configuration.GetConnectionString("DbConnection"));

                return new AWContext(
                    provider.GetRequiredService<ILogger<AWContext>>(),
                    builder.Options,
                    provider.GetRequiredService<IMediator>(),
                    typeof(EmployeeConfiguration).Assembly
                );
            });

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddAutoMapper(typeof(MappingProfile).Assembly, typeof(GetAllEmployeesQuery).Assembly);
            services.AddMediatR(config => config.RegisterServicesFromAssembly(
                typeof(GetAllEmployeesQuery).Assembly)
            );

            return services;
        }

        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());
            hcBuilder.AddElasticsearch(configuration["ElasticSearchUri"]!);

            if (configuration["AuthN:IdP"] == "IdSrv")
                hcBuilder.AddIdentityServer(new Uri(configuration["AuthN:IdSrv:Authority"]!));

            hcBuilder.AddSqlServer(configuration.GetConnectionString("DbConnection")!);

            return services;
        }
    }
}

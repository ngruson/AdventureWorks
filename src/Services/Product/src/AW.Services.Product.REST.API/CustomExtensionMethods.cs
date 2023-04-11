using AW.Services.Infrastructure.Filters;
using AW.Services.Product.Core.Handlers.GetProducts;
using AW.Services.Product.Infrastructure.EFCore.Configurations;
using AW.Services.SharedKernel.EFCore;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.Api;
using AW.SharedKernel.FileHandling;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.OpenIdConnect;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Identity.Web;
using System.IdentityModel.Tokens.Jwt;

namespace AW.Services.Product.REST.API
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

            services
                .AddFluentValidationAutoValidation()
                .AddValidatorsFromAssemblyContaining<GetProductsQueryValidator>();

            services.AddMvcCore()
                .AddApiExplorer();

            return services;
        }

        public static IServiceCollection AddVersioning(this IServiceCollection services)
        {
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
                        options.Audience = "product-api";
                        options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                    });
            }

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerDocumentation("Product API");

            return services;
        }

        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(provider =>
            {
                var builder = new DbContextOptionsBuilder<AWContext>();
                builder.UseSqlServer(configuration.GetConnectionString("DbConnection"));
                builder.EnableSensitiveDataLogging();
                builder.LogTo(Console.WriteLine);

                return new AWContext(
                    provider.GetRequiredService<ILogger<AWContext>>(),
                    builder.Options,
                    provider.GetRequiredService<IMediator>(),
                    typeof(ProductConfiguration).Assembly
                );
            });
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddAutoMapper(typeof(MappingProfile).Assembly, typeof(GetProductsQuery).Assembly);
            services.AddMediatR(config => config.RegisterServicesFromAssembly(
                typeof(GetProductsQuery).Assembly)
            );
            services.AddScoped<IFileHandler, FileHandler>();

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

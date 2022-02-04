using AW.Services.Customer.Infrastructure.EFCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System;
using System.Text.Json.Serialization;
using AutoMapper.EquivalencyExpression;
using AW.SharedKernel.JsonConverters;
using AW.Services.Customer.Core.Handlers.GetCustomers;
using AW.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using AW.SharedKernel.Api;
using AW.Services.SharedKernel.EFCore;
using AW.Services.Infrastructure.Filters;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

namespace AW.Services.Customer.REST.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }    

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCustomMvc()
                .AddCors()
                .AddVersioning()
                .AddCustomAuthentication(Configuration)
                .AddCustomSwagger()
                .AddCustomIntegrations(Configuration)
                .AddCustomHealthCheck(Configuration);            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider, ILoggerFactory loggerFactory)
        {
            var virtualPath = "/customer-api";

            app.Map(virtualPath, builder =>
            {
                if (env.IsDevelopment())
                {
                    builder.UseDeveloperExceptionPage();
                }

                builder.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });

                builder.UseCors("default");
                builder.UseSwaggerDocumentation(virtualPath, Configuration, provider, "Customer API");
                builder.UseRouting();
                builder.UseAuthentication();
                builder.UseAuthorization();
                builder.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                    {
                        Predicate = _ => true,
                        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                    });
                    endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                    {
                        Predicate = r => r.Name.Contains("self")
                    });
                });
            });
        }
    }

    public static class CustomExtensionMethods
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                options.Filters.Add(typeof(ValidateModelStateFilterAttribute));
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                options.JsonSerializerOptions.Converters.Add(
                    new CustomerConverter<
                        Core.Models.GetCustomers.Customer,
                        Core.Models.GetCustomers.StoreCustomer,
                        Core.Models.GetCustomers.IndividualCustomer>()
                );

                options.JsonSerializerOptions.Converters.Add(
                    new CustomerConverter<
                        Models.GetCustomer.Customer,
                        Models.GetCustomer.StoreCustomer,
                        Models.GetCustomer.IndividualCustomer>()
                );

                options.JsonSerializerOptions.Converters.Add(
                    new CustomerConverter<
                        Core.Models.UpdateCustomer.Customer,
                        Core.Models.UpdateCustomer.StoreCustomer,
                        Core.Models.UpdateCustomer.IndividualCustomer>()
                );
            });

            return services;
        }

        public static IServiceCollection AddCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:58093/")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            return services;
        }

        public static IServiceCollection AddVersioning(this IServiceCollection services)
        {
            services.AddMvcCore()
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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration.GetValue<string>("AuthN:Authority");
                    options.Audience = "customer-api";
                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                });

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerDocumentation("Customer API");

            return services;
        }

        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(provider =>
            {
                var builder = new DbContextOptionsBuilder<AWContext>();
                builder.UseSqlServer(configuration.GetConnectionString("DbConnection"));

                return new AWContext(
                    builder.Options,                    
                    provider.GetService<IMediator>(),
                    typeof(EfRepository<>).Assembly
                );
            });

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddAutoMapper(c => c.AddCollectionMappers(), typeof(MappingProfile).Assembly, typeof(GetCustomersQuery).Assembly);
            services.AddMediatR(typeof(GetCustomersQuery));

            return services;
        }

        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());
            //hcBuilder.AddElasticsearch(configuration["ElasticSearchUri"]);
            hcBuilder.AddIdentityServer(new Uri(configuration["AuthN:Authority"]));
            hcBuilder.AddSqlServer(configuration.GetConnectionString("DbConnection"));

            return services;
        }
    }
}
using AW.Services.Infrastructure;
using AW.Services.Infrastructure.Filters;
using AW.Services.Sales.Core.Handlers.GetSalesPersons;
using AW.Services.Sales.Core.IntegrationEvents;
using AW.Services.Sales.Core.IntegrationEvents.Events;
using AW.Services.Sales.Infrastructure.EFCore;
using AW.Services.Sales.SalesPerson.REST.API.Extensions;
using AW.Services.SharedKernel.EFCore;
using AW.SharedKernel.Api;
using AW.SharedKernel.EventBus;
using AW.SharedKernel.EventBus.Abstractions;
using AW.SharedKernel.EventBus.AzureServiceBus;
using AW.SharedKernel.EventBus.EFCore;
using AW.SharedKernel.EventBus.IntegrationEventLog;
using AW.SharedKernel.EventBus.RabbitMQ;
using AW.SharedKernel.Interfaces;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace AW.Services.Sales.SalesPerson.REST.API
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
                .AddVersioning()
                .AddCustomAuthentication(Configuration)
                .AddCustomSwagger()
                .AddCustomDbContext(Configuration)
                .AddCustomIntegrations(Configuration)
                .AddEventBus(Configuration)
                .AddCustomHealthCheck(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            var virtualPath = "/salesperson-api";
            app.Map(virtualPath, builder =>
            {
                builder.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });

                builder.UseSwaggerDocumentation(virtualPath, Configuration, provider, "Sales Person API");
                builder.UseRouting();
                builder.UseAuthentication();
                builder.UseAuthorization();
                builder.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapHealthChecks("/hc", new HealthCheckOptions
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

    static class CustomExtensionsMethods
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration.GetValue<string>("AuthN:Authority");
                    options.Audience = "salesperson-api";
                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                });

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerDocumentation("Sales Person API");

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(provider =>
            {
                var builder = new DbContextOptionsBuilder<AWContext>();
                builder.UseSqlServer(configuration.GetConnectionString("DbConnection"));

                return new AWContext(
                    builder.Options,
                    provider.GetService<IMediator>(),
                    typeof(EfRepository<>).Assembly
                );
            });

            return services;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
            {
                services.AddSingleton<IEventBus, EventBusServiceBus>(sp =>
                {
                    var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersisterConnection>();
                    var logger = sp.GetRequiredService<ILogger<EventBusServiceBus>>();
                    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                    var topicName = configuration["EventBusTopic"];

                    return new EventBusServiceBus(
                        sp,
                        serviceBusPersisterConnection,
                        logger,
                        eventBusSubcriptionsManager,
                        topicName
                    );
                });
            }
            else
            {
                services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
                {
                    var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                    var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                    var queueName = configuration["EventBusQueueName"];
                    var retryCount = 5;
                    if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                    {
                        retryCount = int.Parse(configuration["EventBusRetryCount"]);
                    }

                    return new EventBusRabbitMQ(sp.GetService<IServiceScopeFactory>(),
                        rabbitMQPersistentConnection,
                        logger,
                        eventBusSubcriptionsManager,
                        queueName, retryCount
                    );
                });
            }

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }

        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddAutoMapper(typeof(MappingProfile).Assembly, typeof(GetSalesPersonsQuery).Assembly);
            services.AddMediatR(typeof(GetSalesPersonsQuery));

            services.AddScoped(typeof(IApplication), typeof(Application));
            services.AddScoped<ISalesOrderIntegrationEventService, SalesOrderIntegrationEventService>();
            services.AddIntegrationEventHandlers();
            services.AddScoped<IDbContext>(sp => sp.GetRequiredService<AWContext>());
            services.AddScoped<IIntegrationEventLogService>(sp =>
            {
                var dbContext = sp.GetRequiredService<AWContext>();
                return new IntegrationEventLogService(
                    dbContext,
                    typeof(OrderStartedIntegrationEvent).Assembly
                );
            }
            );

            return services;
        }

        public static IServiceCollection AddIntegrationEventHandlers(this IServiceCollection services)
        {
            var coreAssembly = typeof(GetSalesPersonsQuery).Assembly;
            var types = coreAssembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IIntegrationEventHandler<>))
                )
                .ToList();

            types.ForEach(type =>
            {
                var interfaces = type.GetInterfaces()
                    .Where(i =>
                        i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(IIntegrationEventHandler<>)
                    )
                    .ToList();

                // Has the type implemented any IIntegrationEventHandler<T>?
                if (interfaces.Count > 0)
                {
                    services.AddScoped(interfaces[0], type);
                }
            });

            return services;
        }

        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());
            hcBuilder.AddElasticsearch(configuration["ElasticSearchUri"]);
            hcBuilder.AddIdentityServer(new Uri(configuration["AuthN:Authority"]));
            hcBuilder.AddSqlServer(configuration.GetConnectionString("DbConnection"));

            return services;
        }
    }
}
using AW.Services.Infrastructure;
using AW.Services.Infrastructure.EventBus;
using AW.Services.Infrastructure.EventBus.Abstractions;
using AW.Services.Infrastructure.EventBus.AzureServiceBus;
using AW.Services.Infrastructure.EventBus.IntegrationEventLog;
using AW.Services.Infrastructure.EventBus.RabbitMQ;
using AW.Services.Infrastructure.Filters;
using AW.Services.Sales.Core.Handlers.GetSalesPersons;
using AW.Services.Sales.Core.IntegrationEvents;
using AW.Services.Sales.Core.IntegrationEvents.Events;
using AW.Services.Sales.Infrastructure.EFCore.Configurations;
using AW.Services.SharedKernel.EFCore;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.Api;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.OpenIdConnect;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Identity.Web;

namespace AW.Services.Sales.SalesPerson.REST.API
{
    static class CustomExtensionMethods
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
                        options.Audience = "salesperson-api";
                        options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                    });
            }

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerDocumentationWithVersion("Sales Person API");

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(provider =>
            {
                var builder = new DbContextOptionsBuilder<AWContext>();
                builder.UseSqlServer(configuration.GetConnectionString("DbConnection"));

                return new AWContext(
                    provider.GetRequiredService<ILogger<AWContext>>(),
                    builder.Options,
                    provider.GetRequiredService<IMediator>(),
                    typeof(SalesPersonConfiguration).Assembly
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
                        topicName!
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
                        retryCount = int.Parse(configuration["EventBusRetryCount"]!);
                    }

                    return new EventBusRabbitMQ(sp.GetRequiredService<IServiceScopeFactory>(),
                        rabbitMQPersistentConnection,
                        logger,
                        eventBusSubcriptionsManager,
                        queueName!,
                        retryCount
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
            services.AddMediatR(config => config.RegisterServicesFromAssembly(
                typeof(GetSalesPersonsQuery).Assembly)
            );

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
            });

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
            hcBuilder.AddElasticsearch(configuration["ElasticSearchUri"]!);

            if (configuration["AuthN:IdP"] == "IdSrv")
                hcBuilder.AddIdentityServer(new Uri(configuration["AuthN:IdSrv:Authority"]!));

            hcBuilder.AddSqlServer(configuration.GetConnectionString("DbConnection")!);

            return services;
        }
    }
}

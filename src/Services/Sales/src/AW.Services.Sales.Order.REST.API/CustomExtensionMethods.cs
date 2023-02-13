using AW.Services.Infrastructure.EventBus.Abstractions;
using AW.Services.Infrastructure.EventBus.AzureServiceBus;
using AW.Services.Infrastructure.EventBus.IntegrationEventLog;
using AW.Services.Infrastructure.EventBus.RabbitMQ;
using AW.Services.Infrastructure.EventBus;
using AW.Services.Infrastructure.Filters;
using AW.Services.Infrastructure;
using AW.Services.Sales.Core.Behaviors;
using AW.Services.Sales.Core.Handlers.CancelSalesOrder;
using AW.Services.Sales.Core.Handlers.CreateSalesOrder;
using AW.Services.Sales.Core.Handlers.Identified;
using AW.Services.Sales.Core.Handlers.RejectSalesOrder;
using AW.Services.Sales.Core.Handlers.ShipSalesOrder;
using AW.Services.Sales.Core.Idempotency;
using AW.Services.Sales.Core.IntegrationEvents;
using AW.Services.SharedKernel.EFCore;
using AW.SharedKernel.Api;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.JsonConverters;
using AW.SharedKernel.OpenIdConnect;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using AW.Services.Sales.Order.REST.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using AW.Services.Sales.Infrastructure.EFCore.Configurations;
using AW.Services.SharedKernel.Interfaces;
using AW.Services.Sales.Core.IntegrationEvents.Events;
using AW.Services.Sales.Core.Handlers.GetSalesOrders;
using AW.Services.Sales.Core.Handlers.ApproveSalesOrder;

static class CustomExtensionMethods
{
    public static IServiceCollection AddCustomMvc(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            options.Filters.Add(typeof(ValidateModelStateFilterAttribute));
        });

        services.AddTransient<CustomerConverter<AW.Services.Sales.Core.Models.Customer,
            AW.Services.Sales.Core.Models.StoreCustomer,
            AW.Services.Sales.Core.Models.IndividualCustomer>>();
        services.AddOptions<ConfigureJsonOptions>();
        services.AddSingleton<IConfigureOptions<JsonOptions>, ConfigureJsonOptions>();

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
                    options.Audience = "salesorder-api";
                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                });
        }

        return services;
    }

    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerDocumentation("Sales Order API");

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
                typeof(SalesOrderConfiguration).Assembly
            );;
        });

        return services;
    }

    public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

        //Event handling
        services.AddScoped(typeof(IApplication), typeof(Application));
        services.AddScoped(typeof(IRequestManager), typeof(RequestManager));
        services.AddScoped<ISalesOrderIntegrationEventService, SalesOrderIntegrationEventService>();

        services.AddScoped<IIntegrationEventLogService>(sp =>
        {
            var dbContext = sp.GetRequiredService<AWContext>();
            return new IntegrationEventLogService(
                dbContext,
                typeof(OrderStartedIntegrationEvent).Assembly
            );
        }
        );
        services.AddScoped<IDbContext>(sp => sp.GetRequiredService<AWContext>());
        services.AddIntegrationEventHandlers();

        services.AddAutoMapper(typeof(GetSalesOrdersQuery).Assembly);
        services.AddScoped(
            typeof(IRequestHandler<IdentifiedCommand<CreateSalesOrderCommand, bool>, bool>),
            typeof(IdentifiedCommandHandler<CreateSalesOrderCommand, bool>)
        );
        services.AddScoped(
            typeof(IRequestHandler<IdentifiedCommand<ApproveSalesOrderCommand, bool>, bool>),
            typeof(IdentifiedCommandHandler<ApproveSalesOrderCommand, bool>)
        );
        services.AddScoped(
            typeof(IRequestHandler<IdentifiedCommand<RejectSalesOrderCommand, bool>, bool>),
            typeof(IdentifiedCommandHandler<RejectSalesOrderCommand, bool>)
        );
        services.AddScoped(
            typeof(IRequestHandler<IdentifiedCommand<CancelSalesOrderCommand, bool>, bool>),
            typeof(IdentifiedCommandHandler<CancelSalesOrderCommand, bool>)
        );
        services.AddScoped(
            typeof(IRequestHandler<IdentifiedCommand<ShipSalesOrderCommand, bool>, bool>),
            typeof(IdentifiedCommandHandler<ShipSalesOrderCommand, bool>)
        );
        services.AddMediatR(typeof(GetSalesOrdersQuery));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

        if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
        {
            services.AddSingleton<IServiceBusPersisterConnection>(sp =>
            {
                var serviceBusConnectionString = configuration["EventBusConnection"];

                var topicName = configuration["EventBusTopicName"];
                return new DefaultServiceBusPersisterConnection(
                    serviceBusConnectionString!,
                    topicName!
                );
            });
        }
        else
        {
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = configuration["EventBusConnection"],
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
                {
                    factory.UserName = configuration["EventBusUserName"];
                }

                if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
                {
                    factory.Password = configuration["EventBusPassword"];
                }

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBusRetryCount"]!);
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });
        }

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

    public static IServiceCollection AddIntegrationEventHandlers(this IServiceCollection services)
    {
        var coreAssembly = typeof(GetSalesOrdersQuery).Assembly;
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

        if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
        {
            hcBuilder
                .AddAzureServiceBusTopic(
                    configuration["EventBusConnection"]!,
                    topicName: "event_bus",
                    name: "basket-servicebus-check",
                    tags: new string[] { "servicebus" });
        }
        else
        {
            hcBuilder
                .AddRabbitMQ(
                    $"amqp://{configuration["EventBusConnection"]}",
                    name: "basket-rabbitmqbus-check",
                    tags: new string[] { "rabbitmqbus" });
        }

        if (configuration["AuthN:IdP"] == "IdSrv")
            hcBuilder.AddIdentityServer(new Uri(configuration["AuthN:IdSrv:Authority"]!));

        hcBuilder.AddSqlServer(configuration.GetConnectionString("DbConnection")!);

        return services;
    }
}

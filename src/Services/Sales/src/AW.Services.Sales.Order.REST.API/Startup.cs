using AW.Services.Infrastructure;
using AW.Services.Infrastructure.EventBus;
using AW.Services.Infrastructure.EventBus.Abstractions;
using AW.Services.Infrastructure.EventBus.AzureServiceBus;
using AW.Services.Infrastructure.EventBus.IntegrationEventLog;
using AW.Services.Infrastructure.EventBus.RabbitMQ;
using AW.Services.Infrastructure.Filters;
using AW.Services.Sales.Core.Behaviors;
using AW.Services.Sales.Core.Handlers.ApproveSalesOrder;
using AW.Services.Sales.Core.Handlers.CancelSalesOrder;
using AW.Services.Sales.Core.Handlers.CreateSalesOrder;
using AW.Services.Sales.Core.Handlers.GetSalesOrders;
using AW.Services.Sales.Core.Handlers.Identified;
using AW.Services.Sales.Core.Handlers.RejectSalesOrder;
using AW.Services.Sales.Core.Handlers.ShipSalesOrder;
using AW.Services.Sales.Core.Idempotency;
using AW.Services.Sales.Core.IntegrationEvents;
using AW.Services.Sales.Core.IntegrationEvents.Events;
using AW.Services.Sales.Infrastructure.EFCore.Configurations;
using AW.Services.SharedKernel.EFCore;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.Api;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.JsonConverters;
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
using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AW.Services.Sales.Order.REST.API
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
            var virtualPath = "/salesorder-api";
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

                builder.UseSwaggerDocumentation(virtualPath, Configuration, provider, "Sales Order API");
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

            ConfigureEventBus(app);
        }

        private static void ConfigureEventBus(IApplicationBuilder app)
        {
            var serviceProvider = app.ApplicationServices;
            var scope = serviceProvider.CreateScope();
            var eventBus = scope.ServiceProvider.GetService<IEventBus>();
            eventBus.Subscribe<UserCheckoutAcceptedIntegrationEvent, IIntegrationEventHandler<UserCheckoutAcceptedIntegrationEvent>>();
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
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                options.JsonSerializerOptions.Converters.Add(
                    new CustomerConverter<
                        Core.Models.Customer,
                        Core.Models.StoreCustomer,
                        Core.Models.IndividualCustomer>()
                );
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
                    options.Audience = "salesorder-api";
                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                });

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
                    builder.Options,                    
                    provider.GetService<IMediator>(),
                    typeof(SalesOrderConfiguration).Assembly
                );
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
                        serviceBusConnectionString,
                        topicName
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
                        retryCount = int.Parse(configuration["EventBusRetryCount"]);
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
            hcBuilder.AddElasticsearch(configuration["ElasticSearchUri"]);
            hcBuilder.AddIdentityServer(new Uri(configuration.GetValue<string>("AuthN:Authority")));
            hcBuilder.AddSqlServer(configuration.GetConnectionString("DbConnection"));

            return services;
        }
    }
}
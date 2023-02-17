using AW.Services.Infrastructure.EventBus.Abstractions;
using AW.Services.Sales.Core.IntegrationEvents.Events;
using AW.Services.Sales.Order.REST.API;
using AW.SharedKernel.Api;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((host, serviceProvider, configuration) =>
{
    var config = serviceProvider.GetRequiredService<IConfiguration>();

    configuration
        .MinimumLevel.Verbose()
        .Enrich.WithProperty("ApplicationContext", new Application().AppName)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(
            config["ElasticSearchUri"],
            indexFormat: "aw-logs-{0:yyyy.MM.dd}"
        )
        .ReadFrom.Configuration(config);
});

builder.Services
    .AddCustomMvc()
    .AddVersioning()
    .AddCustomAuthentication(builder.Configuration)
    .AddCustomSwagger()
    .AddCustomDbContext(builder.Configuration)
    .AddEventBus(builder.Configuration)
    .AddCustomIntegrations(builder.Configuration)
    .AddCustomHealthCheck(builder.Configuration);

var app = builder.Build();

var virtualPath = "/salesorder-api";

app.Map(virtualPath, builder =>
{
    builder.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });

    builder.UseCors("default");
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    builder.UseSwaggerDocumentation(virtualPath, app.Configuration, provider, "Sales Order API");
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

ConfigureEventBus(app);

try
{
    Log.Information("Starting web host");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

static void ConfigureEventBus(IApplicationBuilder app)
{
    var serviceProvider = app.ApplicationServices;
    var scope = serviceProvider.CreateScope();
    var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
    eventBus.Subscribe<UserCheckoutAcceptedIntegrationEvent, IIntegrationEventHandler<UserCheckoutAcceptedIntegrationEvent>>();
}

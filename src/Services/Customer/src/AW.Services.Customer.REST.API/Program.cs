using Ardalis.Result.AspNetCore;
using CreateCustomer = AW.Services.Customer.Core.Handlers.CreateCustomer;
using GetCustomers = AW.Services.Customer.Core.Handlers.GetCustomers;
using GetCustomer = AW.Services.Customer.Core.Handlers.GetCustomer;
using AW.Services.Customer.Core.Handlers.GetPreferredAddress;
using UpdateCustomer = AW.Services.Customer.Core.Handlers.UpdateCustomer;
using AW.Services.Customer.REST.API;
using AW.SharedKernel.Api;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using AW.Services.Customer.Core.Handlers.DeleteCustomer;
using System.Text.Json;
using AW.Services.SharedKernel.JsonConverters;
using System.Text.Json.Serialization;
using AW.SharedKernel.JsonConverters;
using AW.SharedKernel.Interfaces;
using AW.Services.Customer.Core.Handlers.AddCustomersToCache;

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
    .AddValidators()
    //Breaking change - Enable this when every API is a minimal API
    //.AddVersioning()
    .AddCaching(builder.Configuration)
    .AddCustomAuthentication(builder.Configuration)
    .AddCustomSwagger()
    .AddCustomIntegrations(builder.Configuration)
    .AddCustomHealthCheck(builder.Configuration);

builder.Services.ConfigureHttpJsonOptions(options => 
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.SerializerOptions.Converters.Add(new CustomerConverter<GetCustomers.Customer,
        GetCustomers.StoreCustomer,
        GetCustomers.IndividualCustomer>());
    options.SerializerOptions.Converters.Add(new CustomerConverter<GetCustomer.Customer,
        GetCustomer.StoreCustomer,
        GetCustomer.IndividualCustomer>());
    options.SerializerOptions.Converters.Add(new CustomerConverter<UpdateCustomer.Customer,
        UpdateCustomer.StoreCustomer,
        UpdateCustomer.IndividualCustomer>());
    options.SerializerOptions.Converters.Add(new EmailAddressConverter());
});

var app = builder.Build();

var virtualPath = "/customer-api";

app.Map(virtualPath, builder =>
{
    builder.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });

    builder.UseCors("default");
    //Breaking change - Enable this when every API is a minimal API
    //var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    builder.UseSwaggerDocumentation(virtualPath, app.Configuration, "Customer API");
    builder.UseRouting();
    builder.UseAuthentication();
    builder.UseAuthorization();
    builder.UseEndpoints(endpoints =>
    {
        endpoints.MapGroup("/customer")
            .MapCustomerApis()
            .WithOpenApi();

        endpoints.MapGroup("/caching")
            .MapCachingApis()
            .WithOpenApi();

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

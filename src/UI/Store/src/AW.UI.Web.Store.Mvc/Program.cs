using AW.SharedKernel.OpenIdConnect;
using AW.UI.Web.Store.Mvc;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

builder.Services.AddRazorPages();
var oidcConfig = new OpenIdConnectConfigurationBuilder(builder.Configuration).Build();

builder.Services
    .AddCustomMvc()
    .AddCustomIntegrations()
    .AddHttpClients(builder.Configuration, oidcConfig!)
    .AddCustomAuthentication(builder.Configuration)
    .AddCustomHealthCheck(builder.Configuration);

var app = builder.Build();

var virtualPath = "/ui-web-store-mvc";

app.Map(virtualPath, builder =>
{
    //builder.UseExceptionHandler("/Error");

    builder.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });

    builder.Use(async (context, next) =>
    {
        context.Request.Scheme = "https";
        await next();
    });

    builder.UseStaticFiles();
    builder.UseCookiePolicy();
    builder.UseRouting();
    builder.UseAuthentication();
    builder.UseAuthorization();

    builder.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

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

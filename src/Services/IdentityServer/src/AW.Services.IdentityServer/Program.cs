using AW.Services.IdentityServer;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCustomMvc()
    .AddDatabase(builder.Configuration)
    .AddIdentity(builder.Configuration)
    .AddCustomHealthCheck(builder.Configuration);

var app = builder.Build();

var virtualPath = "/identityserver";

app.Map(virtualPath, builder =>
{
    builder.UseForwardedHeaders();

    builder.Use(async (context, next) =>
    {
        context.Request.Scheme = "https";
        await next();
    });

    builder.UseStaticFiles();
    builder.UseRouting();
    builder.UseIdentityServer();
    builder.UseAuthorization();
    builder.UseEndpoints(endpoints =>
    {
        endpoints.MapDefaultControllerRoute();
        endpoints.MapRazorPages();
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
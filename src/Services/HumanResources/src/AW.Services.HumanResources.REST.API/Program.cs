using AW.Services.HumanResources.REST.API;
using AW.SharedKernel.Api;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCustomMvc()
    .AddVersioning()
    .AddCustomAuthentication(builder.Configuration)
    .AddCustomSwagger()
    .AddCustomIntegrations(builder.Configuration)
    .AddCustomHealthCheck(builder.Configuration);

var app = builder.Build();

var virtualPath = "/humanresources-api";

app.Map(virtualPath, builder =>
{
    builder.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });

    builder.UseCors("default");
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    builder.UseSwaggerDocumentation(virtualPath, app.Configuration, provider, "Human Resources API");
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

app.Run();
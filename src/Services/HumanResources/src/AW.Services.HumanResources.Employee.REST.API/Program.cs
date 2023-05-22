using Ardalis.Result.AspNetCore;
using CreateEmployee = AW.Services.HumanResources.Core.Handlers.CreateEmployee;
using AW.Services.HumanResources.Core.Handlers.GetEmployee;
using AW.Services.HumanResources.Core.Handlers.GetEmployees;
using UpdateEmployee = AW.Services.HumanResources.Core.Handlers.UpdateEmployee;
using AW.Services.HumanResources.Employee.REST.API;
using AW.Services.HumanResources.SharedKernel;
using AW.SharedKernel.Api;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using AW.Services.HumanResources.Core.Handlers.DeleteEmployee;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCustomMvc()
    .AddValidators()
    //Breaking change - Enable this when every API is a minimal API
    //.AddVersioning()
    .AddCustomAuthentication(builder.Configuration)
    .AddCustomSwagger()
    .AddCustomIntegrations(builder.Configuration)
    .AddCustomHealthCheck(builder.Configuration);

var app = builder.Build();

var virtualPath = "/employee-api";

app.Map(virtualPath, builder =>
{
    builder.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });

    builder.UseCors("default");
    //Breaking change - Enable this when every API is a minimal API
    //var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    builder.UseSwaggerDocumentation(virtualPath, app.Configuration, "Employee API");
    builder.UseRouting();
    builder.UseAuthentication();
    builder.UseAuthorization();
    builder.UseEndpoints(endpoints =>
    {
        endpoints.MapGroup("/employee")
            .MapEmployeeApis()
            .WithOpenApi();

        endpoints.MapGroup("/jobTitles")
            .MapJobTitleApis()
            .WithOpenApi();

        endpoints.MapGroup("/employee/departmentHistory")
            .MapDepartmentHistoryApis()
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

app.Run();

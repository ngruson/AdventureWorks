using Ardalis.Result.AspNetCore;
using CreateDepartment = AW.Services.HumanResources.Core.Handlers.CreateDepartment;
using AW.Services.HumanResources.Core.Handlers.GetDepartment;
using AW.Services.HumanResources.Core.Handlers.GetDepartments;
using UpdateDepartment = AW.Services.HumanResources.Core.Handlers.UpdateDepartment;
using AW.Services.HumanResources.Department.REST.API;
using AW.SharedKernel.Api;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using AW.Services.HumanResources.Core.Handlers.DeleteDepartment;
using AW.Services.HumanResources.SharedKernel;

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

var virtualPath = "/department-api";

app.Map(virtualPath, builder =>
{
    builder.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });

    builder.UseCors("default");
    //Breaking change - Enable this when every API is a minimal API
    //var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    builder.UseSwaggerDocumentation(virtualPath, app.Configuration, "Department API");
    builder.UseRouting();
    builder.UseAuthentication();
    builder.UseAuthorization();
    builder.UseEndpoints(endpoints =>
    {
        endpoints.MapGet("/Department", async ([FromServices] IMediator mediator) =>
            (await mediator.Send(new GetDepartmentsQuery()))
                .ToMinimalApiResult()
        )
        .RequireAuthorization("department-read")
        .WithName("GetDepartments")
        .WithOpenApi();

        endpoints.MapGet("/Department/{objectId}", async (Guid objectId, [FromServices] IMediator mediator) =>
            (await mediator.Send(new GetDepartmentQuery(objectId)))
                .ToMinimalApiResult()
        )
        .RequireAuthorization("department-read")
        .WithName("GetDepartment")
        .WithOpenApi();

        endpoints.MapPost("/Department", async ([FromBody] CreateDepartment.Department department, [FromServices] IMediator mediator) =>
            (await mediator.Send(new CreateDepartment.CreateDepartmentCommand(department)))
                .ToMinimalApiResult()
        )
        .RequireAuthorization("department-write")
        .WithName("CreateDepartment")
        .WithOpenApi();

        endpoints.MapPut("/Department", async ([FromBody] UpdateDepartment.Department department, [FromServices] IMediator mediator) =>
            (await mediator.Send(new UpdateDepartment.UpdateDepartmentCommand(department)))
                .ToMinimalApiResult()
        )
        .RequireAuthorization("department-write")
        .WithName("UpdateDepartment")
        .WithOpenApi();

        endpoints.MapDelete("/Department/{objectId}", async (Guid objectId, [FromServices] IMediator mediator) =>
            (await mediator.Send(new DeleteDepartmentCommand(objectId)))
                .ToMinimalApiResult()
        )
        .RequireAuthorization("department-write")
        .WithName("DeleteDepartment")
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

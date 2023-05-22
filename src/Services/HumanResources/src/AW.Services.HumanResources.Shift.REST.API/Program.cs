using Ardalis.Result.AspNetCore;
using CreateShift = AW.Services.HumanResources.Core.Handlers.CreateShift;
using AW.Services.HumanResources.Core.Handlers.DeleteShift;
using AW.Services.HumanResources.Core.Handlers.GetShift;
using AW.Services.HumanResources.Core.Handlers.GetShifts;
using UpdateShift = AW.Services.HumanResources.Core.Handlers.UpdateShift;
using AW.Services.HumanResources.SharedKernel;
using AW.Services.HumanResources.Shift.REST.API;
using AW.SharedKernel.Api;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;

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

var virtualPath = "/shift-api";

app.Map(virtualPath, builder =>
{
    builder.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });

    builder.UseCors("default");
    //Breaking change - Enable this when every API is a minimal API
    //var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    builder.UseSwaggerDocumentation(virtualPath, app.Configuration, "Shift API");
    builder.UseRouting();
    builder.UseAuthentication();
    builder.UseAuthorization();
    builder.UseEndpoints(endpoints =>
    {
        endpoints.MapGet("/Shift", async ([FromServices] IMediator mediator) =>
            (await mediator.Send(new GetShiftsQuery()))
                .ToMinimalApiResult()
        )
        .RequireAuthorization("shift-read")
        .WithName("GetShifts")
        .WithOpenApi();

        endpoints.MapGet("/Shift/{objectId}", async (Guid objectId, [FromServices] IMediator mediator) =>
            (await mediator.Send(new GetShiftQuery(objectId)))
                .ToMinimalApiResult()
        )
        .RequireAuthorization("shift-read")
        .WithName("GetShift")
        .WithOpenApi();

        endpoints.MapPost("/Shift", async ([FromBody] CreateShift.Shift shift, [FromServices] IMediator mediator) =>
            (await mediator.Send(new CreateShift.CreateShiftCommand(shift)))
                .ToMinimalApiResult()
        )
        .RequireAuthorization("shift-write")
        .WithName("CreateShift")
        .WithOpenApi();

        endpoints.MapPut("/Shift", async ([FromBody] UpdateShift.Shift shift, [FromServices] IMediator mediator) =>
            (await mediator.Send(new UpdateShift.UpdateShiftCommand(shift)))
                .ToMinimalApiResult()
        )
        .RequireAuthorization("shift-write")
        .WithName("UpdateShift")
        .WithOpenApi();

        endpoints.MapDelete("/Shift/{objectId}", async (Guid objectId, [FromServices] IMediator mediator) =>
            (await mediator.Send(new DeleteShiftCommand(objectId)))
                .ToMinimalApiResult()
        )
        .RequireAuthorization("shift-write")
        .WithName("DeleteShift")
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

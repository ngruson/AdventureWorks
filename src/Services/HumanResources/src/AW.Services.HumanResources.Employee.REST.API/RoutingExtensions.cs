using CreateEmployee = AW.Services.HumanResources.Core.Handlers.CreateEmployee;
using AW.Services.HumanResources.Core.Handlers.GetEmployee;
using AW.Services.HumanResources.Core.Handlers.GetEmployees;
using UpdateEmployee = AW.Services.HumanResources.Core.Handlers.UpdateEmployee;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AW.Services.HumanResources.Core.Handlers.DeleteEmployee;
using Ardalis.Result.AspNetCore;
using AW.Services.HumanResources.Core.Handlers.GetJobTitles;
using AW.Services.HumanResources.Core.Handlers.AddDepartmentHistory;
using AW.Services.HumanResources.Core.Handlers.UpdateDepartmentHistory;
using AW.Services.HumanResources.Core.Handlers.DeleteDepartmentHistory;

namespace AW.Services.HumanResources.Employee.REST.API
{
    public static class RoutingExtensions
    {
        public static RouteGroupBuilder MapEmployeeApis(this RouteGroupBuilder group)
        {
            group.MapGet("/", async ([FromServices] IMediator mediator) =>
                (await mediator.Send(new GetEmployeesQuery()))
                    .ToMinimalApiResult()
                )
                .RequireAuthorization("employee-read")
                .WithName("GetEmployees");

            group.MapGet("/{objectId}", async (Guid objectId, [FromServices] IMediator mediator) =>
                (await mediator.Send(new GetEmployeeQuery(objectId)))
                    .ToMinimalApiResult()
                )
                .RequireAuthorization("employee-read")
                .WithName("GetEmployee");

            group.MapPost("/", async ([FromBody] CreateEmployee.Employee employee, [FromServices] IMediator mediator) =>
                (await mediator.Send(new CreateEmployee.CreateEmployeeCommand(employee)))
                    .ToMinimalApiResult()
                )
                .RequireAuthorization("employee-write")
                .WithName("CreateEmployee");

            group.MapPut("/", async ([FromBody] UpdateEmployee.Employee employee, [FromServices] IMediator mediator) =>
                (await mediator.Send(new UpdateEmployee.UpdateEmployeeCommand(employee)))
                    .ToMinimalApiResult()
                )
                .RequireAuthorization("employee-write")
                .WithName("UpdateEmployee");

            group.MapDelete("/{objectId}", async (Guid objectId, [FromServices] IMediator mediator) =>
                (await mediator.Send(new DeleteEmployeeCommand(objectId)))
                    .ToMinimalApiResult()
                )
                .RequireAuthorization("employee-write")
                .WithName("DeleteEmployee");

            return group;
        }

        public static RouteGroupBuilder MapJobTitleApis(this RouteGroupBuilder group)
        {
            group.MapGet("/", async ([FromServices] IMediator mediator) =>
                (await mediator.Send(new GetJobTitlesQuery()))
                    .ToMinimalApiResult()
                )
                .RequireAuthorization("employee-read")
                .WithName("GetJobTitles");

            return group;
        }

        public static RouteGroupBuilder MapDepartmentHistoryApis(this RouteGroupBuilder group)
        {
            group.MapPost("/", async ([FromBody] AddDepartmentHistoryCommand command, [FromServices] IMediator mediator) =>
                (await mediator.Send(command))
                    .ToMinimalApiResult()
                )
                .RequireAuthorization("employee-write")
                .WithName("AddDepartmentHistory");

            group.MapPut("/", async ([FromBody] UpdateDepartmentHistoryCommand command, [FromServices] IMediator mediator) =>
                (await mediator.Send(command))
                    .ToMinimalApiResult()
                )
                .RequireAuthorization("employee-write")
                .WithName("UpdateDepartmentHistory");

            group.MapDelete("/", async (Guid employee, Guid objectId, [FromServices] IMediator mediator) =>
                (await mediator.Send(new DeleteDepartmentHistoryCommand(employee, objectId)))
                    .ToMinimalApiResult()
                )
                .RequireAuthorization("employee-write")
                .WithName("DeleteDepartmentHistory");

            return group;
        }
    }
}

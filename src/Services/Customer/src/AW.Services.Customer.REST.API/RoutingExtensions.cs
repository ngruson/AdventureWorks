using CreateCustomer = AW.Services.Customer.Core.Handlers.CreateCustomer;
using AW.Services.Customer.Core.Handlers.DeleteCustomer;
using GetCustomer = AW.Services.Customer.Core.Handlers.GetCustomer;
using UpdateCustomer = AW.Services.Customer.Core.Handlers.UpdateCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ardalis.Result.AspNetCore;
using AW.Services.Customer.Core.Handlers.GetPreferredAddress;
using AW.Services.Customer.Core.Handlers.GetCustomers;
using AW.Services.Customer.Core.Handlers.AddCustomersToCache;

namespace AW.Services.Customer.REST.API;

public static class RoutingExtensions
{
    public static RouteGroupBuilder MapCustomerApis(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromServices] IMediator mediator) =>
            (await mediator.Send(new GetCustomersQuery()))
                .ToMinimalApiResult()
            )
            .RequireAuthorization("customer-read")
            .WithName("GetCustomers")
            .WithOpenApi();

        group.MapGet("/{objectId}", async (Guid objectId, [FromServices] IMediator mediator) =>

            (await mediator.Send(new GetCustomer.GetCustomerQuery(objectId)))
                .ToMinimalApiResult()
            )
            .RequireAuthorization("customer-read")
            .WithName("GetCustomer")
            .WithOpenApi();

        group.MapGet("/{objectId}/preferredAddress/{addressType}", async (Guid objectId, string addressType, [FromServices] IMediator mediator) =>
            (await mediator.Send(new GetPreferredAddressQuery(objectId, addressType)))
                .ToMinimalApiResult()
            )
            .RequireAuthorization("customer-read")
            .WithName("GetPreferredAddress")
            .WithOpenApi();

        group.MapPost("/", async ([FromBody] CreateCustomer.Customer customer, [FromServices] IMediator mediator) =>
            (await mediator.Send(new CreateCustomer.CreateCustomerCommand(customer)))
                .ToMinimalApiResult()
            )
            .RequireAuthorization("customer-write")
            .WithName("CreateCustomer");

        group.MapPut("/{objectId}", async (Guid objectId, [FromBody] UpdateCustomer.Customer customer, [FromServices] IMediator mediator) =>
            (await mediator.Send(new UpdateCustomer.UpdateCustomerCommand(customer)))
                .ToMinimalApiResult()
            )
            .RequireAuthorization("customer-write")
            .WithName("UpdateCustomer");

        group.MapDelete("/{objectId}", async (Guid objectId, [FromServices] IMediator mediator) =>
            (await mediator.Send(new DeleteCustomerCommand(objectId)))
                .ToMinimalApiResult()
            )
            .RequireAuthorization("customer-write")
            .WithName("DeleteCustomer");

        return group;
    }

    public static RouteGroupBuilder MapCachingApis(this RouteGroupBuilder group)
    {
        group.MapPost("/populateCache", async ([FromServices] IMediator mediator) =>
            (await mediator.Send(new AddCustomersToCacheCommand()))
                .ToMinimalApiResult()
            )
            .RequireAuthorization("customer-read")
            .WithName("PopulateCache")
            .WithOpenApi();

        return group;
    }
}

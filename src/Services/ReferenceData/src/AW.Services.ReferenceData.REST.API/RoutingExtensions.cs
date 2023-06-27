using Ardalis.Result.AspNetCore;
using AW.Services.ReferenceData.Core.Handlers.AddressType.GetAddressTypes;
using AW.Services.ReferenceData.Core.Handlers.ContactType.GetContactTypes;
using AW.Services.ReferenceData.Core.Handlers.CountryRegion.GetCountries;
using AW.Services.ReferenceData.Core.Handlers.ShipMethod.GetShipMethods;
using AW.Services.ReferenceData.Core.Handlers.StateProvince.GetStatesProvinces;
using AW.Services.ReferenceData.Core.Handlers.Territory.GetTerritories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AW.Services.ReferenceData.REST.API;

public static class RoutingExtensions
{
    public static RouteGroupBuilder MapAddressTypeApis(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromServices] IMediator mediator) =>
            (await mediator.Send(new GetAddressTypesQuery()))
                .ToMinimalApiResult()
            )
            .RequireAuthorization("referencedata-read")
            .WithName("GetAddressTypes")
            .WithOpenApi();

        return group;
    }

    public static RouteGroupBuilder MapContactTypeApis(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromServices] IMediator mediator) =>
            (await mediator.Send(new GetContactTypesQuery()))
                .ToMinimalApiResult()
            )
            .RequireAuthorization("referencedata-read")
            .WithName("GetContactTypes")
            .WithOpenApi();

        return group;
    }

    public static RouteGroupBuilder MapCountryRegionApis(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromServices] IMediator mediator) =>
            (await mediator.Send(new GetCountriesQuery()))
                .ToMinimalApiResult()
            )
            .RequireAuthorization("referencedata-read")
            .WithName("GetCountries")
            .WithOpenApi();

        return group;
    }

    public static RouteGroupBuilder MapShipMethodApis(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromServices] IMediator mediator) =>
            (await mediator.Send(new GetShipMethodsQuery()))
                .ToMinimalApiResult()
            )
            .RequireAuthorization("referencedata-read")
            .WithName("GetShipMethods")
            .WithOpenApi();

        return group;
    }

    public static RouteGroupBuilder MapStateProvinceApis(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromServices] IMediator mediator, string? countryRegionCode) =>
            (await mediator.Send(new GetStatesProvincesQuery(countryRegionCode)))
                .ToMinimalApiResult()
            )
            .RequireAuthorization("referencedata-read")
            .WithName("GetStatesProvinces")
            .WithOpenApi();

        return group;
    }

    public static RouteGroupBuilder MapTerritoryApis(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromServices] IMediator mediator, string? countryRegionCode) =>
            (await mediator.Send(new GetTerritoriesQuery(countryRegionCode)))
                .ToMinimalApiResult()
            )
            .RequireAuthorization("referencedata-read")
            .WithName("GetTerritories")
            .WithOpenApi();

        return group;
    }
}

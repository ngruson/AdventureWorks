using Ardalis.Result;
using MediatR;

namespace AW.Services.ReferenceData.Core.Handlers.Territory.GetTerritories;

public class GetTerritoriesQuery : IRequest<Result<List<Territory>>>
{
    public GetTerritoriesQuery(string? countryRegionCode)
    {
        CountryRegionCode = countryRegionCode;
    }
    public string? CountryRegionCode { get; set; }
}

using Ardalis.Result;
using MediatR;

namespace AW.Services.ReferenceData.Core.Handlers.StateProvince.GetStatesProvinces;

public class GetStatesProvincesQuery : IRequest<Result<List<StateProvince>>>
{
    public GetStatesProvincesQuery(string? countryRegionCode)
    {
        CountryRegionCode = countryRegionCode;
    }
    public string? CountryRegionCode { get; set; }
}

using MediatR;

namespace AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetStatesProvinces
{
    public class GetStatesProvincesQuery : IRequest<List<StateProvince>>
    {
        public GetStatesProvincesQuery(string? countryRegionCode) => CountryRegionCode = countryRegionCode;

        public string? CountryRegionCode { get; set; }
    }
}
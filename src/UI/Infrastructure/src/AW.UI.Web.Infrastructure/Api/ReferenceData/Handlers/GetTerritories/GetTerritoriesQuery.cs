using MediatR;

namespace AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetTerritories
{
    public class GetTerritoriesQuery : IRequest<List<Territory>>
    {
        public GetTerritoriesQuery(string? countryRegionCode = null)
        {
            CountryRegionCode = countryRegionCode;
        }

        public string? CountryRegionCode { get; init; }
    }
}
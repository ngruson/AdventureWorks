using Ardalis.Specification;

namespace AW.Services.ReferenceData.Core.Specifications
{
    public class GetTerritoriesForCountrySpecification : Specification<Entities.Territory>
    {
        public GetTerritoriesForCountrySpecification(string countryRegionCode) : base()
        {
            Query
                .Where(p => p.CountryRegionCode == countryRegionCode);

            Query.OrderBy(_ => _.Name);
        }
    }
}
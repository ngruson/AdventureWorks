using Ardalis.Specification;

namespace AW.Services.ReferenceData.Core.Specifications
{
    public class GetStatesProvincesForCountrySpecification : Specification<Core.Entities.StateProvince>
    {
        public GetStatesProvincesForCountrySpecification(string countryRegionCode) : base()
        {
            Query
                .Where(p => p.CountryRegionCode == countryRegionCode)
                .Include(p => p.CountryRegion);
        }
    }
}
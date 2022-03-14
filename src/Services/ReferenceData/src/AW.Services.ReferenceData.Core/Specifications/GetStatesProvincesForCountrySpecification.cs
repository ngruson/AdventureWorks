using Ardalis.Specification;

namespace AW.Services.ReferenceData.Core.Specifications
{
    public class GetStatesProvincesForCountrySpecification : Specification<Entities.StateProvince>
    {
        public GetStatesProvincesForCountrySpecification(string countryRegionCode) : base()
        {
            Query
                .Where(p => p.CountryRegion.CountryRegionCode == countryRegionCode)
                .Include(p => p.CountryRegion);
        }
    }
}
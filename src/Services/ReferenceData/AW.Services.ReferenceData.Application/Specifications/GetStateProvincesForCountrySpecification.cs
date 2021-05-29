using Ardalis.Specification;

namespace AW.Services.ReferenceData.Application.Specifications
{
    public class GetStatesProvincesForCountrySpecification : Specification<Domain.StateProvince>
    {
        public GetStatesProvincesForCountrySpecification(string countryRegionCode) : base()
        {
            Query
                .Where(p => p.CountryRegionCode == countryRegionCode)
                .Include(p => p.CountryRegion);
        }
    }
}
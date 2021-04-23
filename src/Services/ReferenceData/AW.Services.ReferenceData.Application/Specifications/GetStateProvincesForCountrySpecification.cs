using Ardalis.Specification;

namespace AW.Services.ReferenceData.Application.Specifications
{
    public class GetStateProvincesForCountrySpecification : Specification<Domain.StateProvince>
    {
        public GetStateProvincesForCountrySpecification(string countryRegionCode) : base()
        {
            Query
                .Where(p => p.CountryRegionCode == countryRegionCode)
                .Include(p => p.CountryRegion);
        }
    }
}
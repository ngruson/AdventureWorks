using Ardalis.Specification;

namespace AW.Services.ReferenceData.Core.Specifications
{
    public class GetStatesProvincesSpecification : Specification<Entities.StateProvince>
    {
        public GetStatesProvincesSpecification(string? countryRegionCode = null) : base()
        {
            if (!string.IsNullOrEmpty(countryRegionCode))
                Query.Where(p => p.CountryRegion.CountryRegionCode == countryRegionCode);

            Query
                .Include(p => p.CountryRegion);
        }
    }
}
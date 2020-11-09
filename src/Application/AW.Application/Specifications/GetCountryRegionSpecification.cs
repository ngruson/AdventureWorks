using Ardalis.Specification;

namespace AW.Application.Specifications
{
    public class GetCountryRegionSpecification : Specification<Domain.Person.StateProvince>
    {
        public GetCountryRegionSpecification(string countryRegionCode) : base()
        {
            Query
                .Where(c => c.CountryRegionCode == countryRegionCode);
        }
    }
}
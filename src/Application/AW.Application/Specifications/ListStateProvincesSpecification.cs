using Ardalis.Specification;

namespace AW.Application.Specifications
{
    public class ListStateProvincesSpecification : Specification<Domain.Person.StateProvince>
    {
        public ListStateProvincesSpecification(string countryRegionCode) : base()
        {
            Query
                .Where(sp => sp.CountryRegionCode == countryRegionCode);
        }
    }
}
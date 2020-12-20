using Ardalis.Specification;

namespace AW.Core.Application.Specifications
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
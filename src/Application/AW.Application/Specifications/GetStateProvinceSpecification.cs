using Ardalis.Specification;

namespace AW.Application.Specifications
{
    public class GetStateProvinceSpecification : Specification<Domain.Person.StateProvince>
    {
        public GetStateProvinceSpecification(string stateProvinceCode) : base()
        {
            Query
                .Where(c => c.StateProvinceCode == stateProvinceCode);
        }
    }
}
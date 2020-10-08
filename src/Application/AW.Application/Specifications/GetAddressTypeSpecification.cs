using Ardalis.Specification;

namespace AW.Application.Specifications
{
    public class GetAddressTypeSpecification : Specification<Domain.Person.AddressType>
    {
        public GetAddressTypeSpecification(string name) : base()
        {
            Query
                .Where(c => c.Name == name);
        }
    }
}
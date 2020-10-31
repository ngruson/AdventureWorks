using Ardalis.Specification;

namespace AW.Application.Specifications
{
    public class ListAddressTypesSpecification : Specification<Domain.Person.AddressType>
    {
        public ListAddressTypesSpecification() : base()
        {
            Query
                .OrderBy(at => at.Id);
        }
    }
}
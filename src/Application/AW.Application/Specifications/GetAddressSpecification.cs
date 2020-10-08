using Ardalis.Specification;

namespace AW.Application.Specifications
{
    public class GetAddressSpecification : Specification<Domain.Person.Address>
    {
        public GetAddressSpecification(
            string addressline1,
            string addressLine2,
            string city,
            int stateProvinceID,
            string postalCode) : base()
        {
            Query
                .Where(a => a.AddressLine1 == addressline1 &&
                    a.AddressLine2 == addressLine2 &&
                    a.City == city &&
                    a.StateProvinceID == stateProvinceID &&
                    a.PostalCode == postalCode
                );
        }
    }
}
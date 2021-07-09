using Ardalis.Specification;
using AW.Services.Customer.Core.Entities;

namespace AW.Services.Customer.Core.Specifications
{
    public class GetAddressesByPostalCodeSpecification : Specification<Address>
    {
        public GetAddressesByPostalCodeSpecification(string postalCode) : base()
        {
            Query
                .Where(a => 
                    a.PostalCode == postalCode
                );
        }
    }
}
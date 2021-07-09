using Ardalis.Specification;
using AW.Services.Customer.Core.Entities;

namespace AW.Services.Customer.Core.Specifications
{
    public class GetAddressSpecification : Specification<Address>, ISingleResultSpecification
    {
        public GetAddressSpecification(
            string addressline1,
            string addressLine2,
            string postalCode,
            string city,
            string stateProvinceCode,
            string countryRegionCode
        ) : base()
        {
            Query
                .Where(a => 
                    a.AddressLine1 == addressline1 &&
                    a.AddressLine2 == addressLine2 &&
                    a.PostalCode == postalCode &&
                    a.City == city &&
                    a.StateProvinceCode == stateProvinceCode &&
                    a.CountryRegionCode == countryRegionCode
                );
        }
    }
}
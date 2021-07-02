using Ardalis.Specification;

namespace AW.Services.Customer.Application.Specifications
{
    #if NETSTANDARD2_0
    public class GetAddressSpecification : Specification<Domain.Address>
    #elif NETSTANDARD2_1_OR_GREATER
    public class GetAddressSpecification : Specification<Domain.Address>, ISingleResultSpecification
    #endif
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
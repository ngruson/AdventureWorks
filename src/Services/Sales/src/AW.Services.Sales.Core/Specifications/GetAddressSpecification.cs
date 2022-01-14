using Ardalis.Specification;

namespace AW.Services.Sales.Core.Specifications
{
    public class GetAddressSpecification : Specification<Entities.Address>, ISingleResultSpecification
    {
        public GetAddressSpecification(
            string addressLine1,
            string addressLine2,
            string postalCode,
            string city,
            string stateProvinceCode,
            string countryRegionCode
            )
        {
            Query.Where(a => a.AddressLine1 == addressLine1
                && a.AddressLine2 == addressLine2
                && a.PostalCode == postalCode
                && a.City == city
                && a.StateProvinceCode == stateProvinceCode
                && a.CountryRegionCode == countryRegionCode
            );
        }
    }
}
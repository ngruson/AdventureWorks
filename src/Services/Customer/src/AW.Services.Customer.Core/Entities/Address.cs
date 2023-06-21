using AW.Services.SharedKernel.Interfaces;

namespace AW.Services.Customer.Core.Entities
{
    public class Address : IAggregateRoot
    {
        public Address()
        {
        }

        public Address(string addressLine1, string addressLine2, string postalCode, string city, string stateProvinceCode, string countryRegionCode)
        {
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            PostalCode = postalCode;
            City = city;
            StateProvinceCode = stateProvinceCode;
            CountryRegionCode = countryRegionCode;
        }

        public int Id { get; set; }
        public Guid ObjectId { get; set; }
        public string? AddressLine1 { get; private set; }
        public string? AddressLine2 { get; private set; }
        public string? PostalCode { get; private set; }
        public string? City { get; private set; }
        public string? StateProvinceCode { get; private set; }
        public string? CountryRegionCode { get; private set; }
    }
}

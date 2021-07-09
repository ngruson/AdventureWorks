using AW.Services.SalesOrder.Core.Entities;

namespace AW.Services.SalesOrder.Core.UnitTests.TestBuilders
{
    public class AddressBuilder
    {
        private Address address = new();

        public AddressBuilder AddressLine1(string addressLine1)
        {
            address.AddressLine1 = addressLine1;
            return this;
        }

        public AddressBuilder PostalCode(string postalCode)
        {
            address.PostalCode = postalCode;
            return this;
        }

        public AddressBuilder City(string city)
        {
            address.City = city;
            return this;
        }

        public AddressBuilder StateProvinceCode(string stateProvinceCode)
        {
            address.StateProvinceCode = stateProvinceCode;
            return this;
        }

        public AddressBuilder CountryRegionCode(string countryRegionCode)
        {
            address.CountryRegionCode = countryRegionCode;
            return this;
        }

        public Address Build()
        {
            return address;
        }

        public AddressBuilder WithTestValues()
        {
            address = new Address
            {
                AddressLine1 = "42525 Austell Road",
                PostalCode = "30106",
                City = "Austell",
                StateProvinceCode = "GA",
                CountryRegionCode = "US"
            };

            return this;
        }
    }
}
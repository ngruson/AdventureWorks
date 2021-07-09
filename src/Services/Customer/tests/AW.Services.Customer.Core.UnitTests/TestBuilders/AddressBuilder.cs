using AW.Services.Customer.Core.Entities;

namespace AW.Services.Customer.Core.UnitTests.TestBuilders
{
    public class AddressBuilder
    {
        private Address address = new();

        public AddressBuilder AddressLine1(string addressLine1)
        {
            address.AddressLine1 = addressLine1;
            return this;
        }

        public AddressBuilder AddressLine2(string addressLine2)
        {
            address.AddressLine2 = addressLine2;
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
                AddressLine1 = "3761 N. 14th St",
                PostalCode = "4700",
                City = "Rockhampton",
                StateProvinceCode = "QLD",
                CountryRegionCode = "AU"
            };

            return this;
        }
    }
}
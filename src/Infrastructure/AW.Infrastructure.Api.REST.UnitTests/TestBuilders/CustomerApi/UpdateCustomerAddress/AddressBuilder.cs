using AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerAddress;

namespace AW.Infrastructure.Api.REST.UnitTests.TestBuilders.CustomerApi.UpdateCustomerAddress
{
    public class AddressBuilder
    {
        private Address address = new Address();

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

        public AddressBuilder City(string city)
        {
            address.City = city;
            return this;
        }

        public AddressBuilder StateProvince(string stateProvinceCode)
        {
            address.StateProvinceCode = stateProvinceCode;
            return this;
        }

        public AddressBuilder PostalCode(string postalCode)
        {
            address.PostalCode = postalCode;
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
                AddressLine1 = "2251 Elliot Avenue",
                City = "Seattle",
                StateProvinceCode = "WA",
                PostalCode = "98104"
            };

            return this;
        }
    }
}
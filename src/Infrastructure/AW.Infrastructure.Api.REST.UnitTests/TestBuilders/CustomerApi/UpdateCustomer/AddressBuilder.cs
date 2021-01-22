using AW.Core.Abstractions.Api.CustomerApi.UpdateCustomer;

namespace AW.Infrastructure.Api.REST.UnitTests.TestBuilders.CustomerApi.UpdateCustomer
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

        public AddressBuilder StateProvince(StateProvince stateProvince)
        {
            address.StateProvince = stateProvince;
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
                StateProvince = new StateProvinceBuilder().WithTestValues().Build(),
                PostalCode = "98104"
            };

            return this;
        }
    }
}
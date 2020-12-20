using AW.Core.Abstractions.Api.CustomerApi.GetCustomer;

namespace AW.UI.Web.Internal.UnitTests.TestBuilders.GetCustomer
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
            address.AddressLine1 = addressLine2;
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

        public AddressBuilder StateProvince(StateProvince stateProvince)
        {
            address.StateProvince = stateProvince;
            return this;
        }

        public Address Build()
        {
            return address;
        }
    }
}
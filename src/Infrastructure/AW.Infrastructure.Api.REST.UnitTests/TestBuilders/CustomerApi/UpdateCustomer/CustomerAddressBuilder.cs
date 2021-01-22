using AW.Core.Abstractions.Api.CustomerApi.UpdateCustomer;

namespace AW.Infrastructure.Api.REST.UnitTests.TestBuilders.CustomerApi.UpdateCustomer
{
    public class CustomerAddressBuilder
    {
        private CustomerAddress customerAddress = new CustomerAddress();

        public CustomerAddressBuilder Address(Address address)
        {
            customerAddress.Address = address;
            return this;
        }

        public CustomerAddressBuilder AddressType(string addressType)
        {
            customerAddress.AddressType = addressType;
            return this;
        }

        public CustomerAddress Build()
        {
            return customerAddress;
        }

        public CustomerAddressBuilder WithTestValues()
        {
            customerAddress = new CustomerAddress
            {
                Address = new AddressBuilder().WithTestValues().Build(),
                AddressType = "Main Office"
            };

            return this;
        }
    }
}
using AW.Services.Customer.Domain;

namespace AW.Services.Customer.Application.UnitTests.TestBuilders
{
    public class CustomerAddressBuilder
    {
        private CustomerAddress customerAddress = new CustomerAddress();

        public CustomerAddressBuilder AddressType(string addressType)
        {
            customerAddress.AddressType = addressType;
            return this;
        }

        public CustomerAddressBuilder Address(Address address)
        {
            customerAddress.Address = address;
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
                AddressType = "Home",
                Address = new AddressBuilder()
                    .WithTestValues()
                    .Build()
            };

            return this;
        }
    }
}
using AW.Services.Customer.Core.Handlers.GetCustomers;

namespace AW.Services.Customer.WCF.UnitTests.TestBuilders.GetCustomers
{
    public class CustomerAddressBuilder
    {
        private CustomerAddressDto customerAddress = new CustomerAddressDto();

        public CustomerAddressBuilder AddressType(string addressType)
        {
            customerAddress.AddressType = addressType;
            return this;
        }

        public CustomerAddressBuilder Address(AddressDto address)
        {
            customerAddress.Address = address;
            return this;
        }

        public CustomerAddressDto Build()
        {
            return customerAddress;
        }

        public CustomerAddressBuilder WithTestValues()
        {
            customerAddress = new CustomerAddressDto
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
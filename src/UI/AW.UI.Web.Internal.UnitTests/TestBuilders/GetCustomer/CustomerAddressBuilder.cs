using AW.Core.Abstractions.Api.CustomerApi.GetCustomer;

namespace AW.UI.Web.Internal.UnitTests.TestBuilders.GetCustomer
{
    public class CustomerAddressBuilder
    {
        private CustomerAddress customerAddress = new CustomerAddress();

        public CustomerAddressBuilder AddressTypeName(string addressTypeName)
        {
            customerAddress.AddressType = addressTypeName;
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
    }
}
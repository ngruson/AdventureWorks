﻿using AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerAddress;

namespace AW.Infrastructure.Api.REST.UnitTests.TestBuilders.CustomerApi.UpdateCustomerAddress
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
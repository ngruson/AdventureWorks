﻿using AW.UI.Web.Common.ApiClients.CustomerApi.Models.GetCustomers;

namespace AW.UI.Web.Internal.UnitTests.TestBuilders.GetCustomers
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

        public CustomerAddressBuilder WithTestValues()
        {
            customerAddress = new CustomerAddress
            {
                AddressType = "Main Office",
                Address = new AddressBuilder()
                    .WithTestValues()
                    .Build()
            };

            return this;
        }

        public CustomerAddress Build()
        {
            return customerAddress;
        }
    }
}
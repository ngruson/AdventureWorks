﻿using AW.Core.Abstractions.Api.CustomerApi.UpdateCustomer;
using System.Collections.Generic;

namespace AW.Infrastructure.Api.REST.UnitTests.TestBuilders.CustomerApi.UpdateCustomer
{
    public class StoreBuilder
    {
        private Store store = new Store();

        public StoreBuilder Name(string name)
        {
            store.Name = name;
            return this;
        }

        public StoreBuilder SalesPerson(SalesPerson salesPerson)
        {
            store.SalesPerson = salesPerson;
            return this;
        }

        public StoreBuilder Addresses(List<CustomerAddress> addresses)
        {
            store.Addresses = addresses;
            return this;
        }

        public StoreBuilder Contacts(List<CustomerContact> contacts)
        {
            store.Contacts = contacts;
            return this;
        }

        public Store Build()
        {
            return store;
        }

        public StoreBuilder WithTestValues()
        {
            store = new Store
            {
                Name = "A Bike Store",
                SalesPerson = new SalesPersonBuilder().WithTestValues().Build()
            };

            return this;
        }
    }
}
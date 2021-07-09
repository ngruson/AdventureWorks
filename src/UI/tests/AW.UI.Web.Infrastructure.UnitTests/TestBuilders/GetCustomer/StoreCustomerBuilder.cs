using AW.SharedKernel.Interfaces;
using AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models.GetCustomer;
using System.Collections.Generic;

namespace AW.UI.Web.Infrastructure.UnitTests.TestBuilders.GetCustomer
{
    public class StoreCustomerBuilder
    {
        private StoreCustomer customer = new StoreCustomer();

        public StoreCustomerBuilder CustomerType(CustomerType customerType)
        {
            customer.CustomerType = customerType;
            return this;
        }

        public StoreCustomerBuilder AccountNumber(string accountNumber)
        {
            customer.AccountNumber = accountNumber;
            return this;
        }

        public StoreCustomerBuilder Territory(string territory)
        {
            customer.Territory = territory;
            return this;
        }

        public StoreCustomerBuilder Name(string name)
        {
            customer.Name = name;
            return this;
        }

        public StoreCustomerBuilder SalesPerson(string salesPerson)
        {
            customer.SalesPerson = salesPerson;
            return this;
        }

        public StoreCustomerBuilder Addresses(List<CustomerAddress> addresses)
        {
            customer.Addresses = addresses;
            return this;
        }

        public StoreCustomerBuilder Contacts(List<StoreCustomerContact> contacts)
        {
            customer.Contacts = contacts;
            return this;
        }

        public StoreCustomer Build()
        {
            return customer;
        }

        public StoreCustomerBuilder WithTestValues()
        {
            customer = new StoreCustomer
            {
                CustomerType = SharedKernel.Interfaces.CustomerType.Store,
                AccountNumber = "AW00000001",
                Name = "A Bike Store",
                Territory = "Northwest",
                SalesPerson = "Pamela O Ansman-Wolfe",
                Addresses = new List<CustomerAddress>
                {
                    new CustomerAddressBuilder()
                        .WithTestValues()
                        .Build()
                },
                Contacts = new List<StoreCustomerContact>
                {
                    new StoreCustomerContactBuilder()
                        .WithTestValues()
                        .Build()
                },
                SalesOrders = new List<SalesOrder>
                {
                    new SalesOrderBuilder()
                        .WithTestValues()
                        .Build()
                }
            };

            return this;
        }
    }
}
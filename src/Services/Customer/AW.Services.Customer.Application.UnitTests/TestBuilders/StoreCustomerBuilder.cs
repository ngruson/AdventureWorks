using AW.Services.Customer.Domain;
using System.Collections.Generic;

namespace AW.Services.Customer.Application.UnitTests.TestBuilders
{
    public class StoreCustomerBuilder
    {
        private StoreCustomer storeCustomer = new StoreCustomer();

        public StoreCustomerBuilder AccountNumber(string accountNumber)
        {
            storeCustomer.AccountNumber = accountNumber;
            return this;
        }

        public StoreCustomerBuilder Territory(string territory)
        {
            storeCustomer.Territory = territory;
            return this;
        }

        public StoreCustomerBuilder Addresses(List<CustomerAddress> addresses)
        {
            storeCustomer.Addresses = addresses;
            return this;
        }

        public StoreCustomerBuilder SalesOrders(List<SalesOrder> salesOrders)
        {
            storeCustomer.SalesOrders = salesOrders;
            return this;
        }

        public StoreCustomerBuilder Name(string name)
        {
            storeCustomer.Name = name;
            return this;
        }

        public StoreCustomerBuilder SalesPerson(string salesPerson)
        {
            storeCustomer.SalesPerson = salesPerson;
            return this;
        }

        public StoreCustomerBuilder Contacts(List<StoreCustomerContact> contacts)
        {
            storeCustomer.Contacts = contacts;
            return this;
        }

        public StoreCustomer Build()
        {
            return storeCustomer;
        }

        public StoreCustomerBuilder WithTestValues()
        {
            storeCustomer = new StoreCustomer
            {
                AccountNumber = "AW00000001",
                Territory = "Northwest",
                Name = "A Bike Store",
                SalesPerson = "Pamela O Ansman-Wolfe",
                Contacts = new List<StoreCustomerContact>
                {
                    new StoreCustomerContactBuilder()
                    .WithTestValues()
                    .Build()
                }
            };

            return this;
        }
    }
}
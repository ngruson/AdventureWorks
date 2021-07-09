using AW.Services.Customer.Core.Handlers.GetCustomer;
using System.Collections.Generic;

namespace AW.Services.Customer.WCF.UnitTests.TestBuilders.GetCustomer
{
    public class StoreCustomerBuilder
    {
        private StoreCustomerDto storeCustomer = new StoreCustomerDto();

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

        public StoreCustomerBuilder Addresses(List<CustomerAddressDto> addresses)
        {
            storeCustomer.Addresses = addresses;
            return this;
        }

        public StoreCustomerBuilder SalesOrders(List<SalesOrderDto> salesOrders)
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

        public StoreCustomerBuilder Contacts(List<StoreCustomerContactDto> contacts)
        {
            storeCustomer.Contacts = contacts;
            return this;
        }

        public StoreCustomerDto Build()
        {
            return storeCustomer;
        }

        public StoreCustomerBuilder WithTestValues()
        {
            storeCustomer = new StoreCustomerDto
            {
                AccountNumber = "AW00000001",
                Territory = "Northwest",
                Name = "A Bike Store",
                SalesPerson = "Pamela O Ansman-Wolfe",
                Contacts = new List<StoreCustomerContactDto>
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
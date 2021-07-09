using AW.Services.Customer.Core.Handlers.GetCustomers;
using System.Collections.Generic;

namespace AW.Services.Customer.REST.API.UnitTests.TestBuilders.GetCustomers
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
                Addresses = new List<CustomerAddressDto>
                {
                    new CustomerAddressBuilder()
                        .AddressType("Main Office")
                        .Address(new AddressBuilder()
                            .AddressLine1("2251 Elliot Avenue")
                            .PostalCode("98104")
                            .City("Seattle")
                            .StateProvinceCode("WA")
                            .CountryRegionCode("US")
                            .Build()
                         )
                        .Build()
                },
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

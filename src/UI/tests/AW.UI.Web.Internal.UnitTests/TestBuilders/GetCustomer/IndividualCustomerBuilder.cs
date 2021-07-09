using AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models.GetCustomer;
using System.Collections.Generic;

namespace AW.UI.Web.Internal.UnitTests.TestBuilders.GetCustomer
{
    public class IndividualCustomerBuilder
    {
        private IndividualCustomer customer = new IndividualCustomer();

        public IndividualCustomerBuilder AccountNumber(string accountNumber)
        {
            customer.AccountNumber = accountNumber;
            return this;
        }

        public IndividualCustomerBuilder Person(Person person)
        {
            customer.Person = person;
            return this;
        }

        public IndividualCustomerBuilder Addresses(List<CustomerAddress> addresses)
        {
            customer.Addresses = addresses;
            return this;
        }

        public IndividualCustomer Build()
        {
            return customer;
        }

        public IndividualCustomerBuilder WithTestValues()
        {
            customer = new IndividualCustomer
            {
                AccountNumber = "AW00000001",
                Person = new Person
                {
                    FirstName = "Jon",
                    MiddleName = "V",
                    LastName = "Yang"
                },
                Addresses = new List<CustomerAddress>
                {
                    new CustomerAddress
                    {
                        AddressType = "Home",
                        Address = new Address
                        {
                            StateProvinceCode = "AZ",
                            CountryRegionCode = "US"
                        }
                    }
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
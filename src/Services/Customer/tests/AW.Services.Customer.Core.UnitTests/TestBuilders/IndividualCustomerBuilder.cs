using AW.Services.Customer.Core.Entities;
using System.Collections.Generic;

namespace AW.Services.Customer.Core.UnitTests.TestBuilders
{
    public class IndividualCustomerBuilder
    {
        private IndividualCustomer individualCustomer = new();

        public IndividualCustomerBuilder AccountNumber(string accountNumber)
        {
            individualCustomer.AccountNumber = accountNumber;
            return this;
        }

        public IndividualCustomerBuilder Territory(string territory)
        {
            individualCustomer.Territory = territory;
            return this;
        }

        public IndividualCustomerBuilder Addresses(List<CustomerAddress> addresses)
        {
            individualCustomer.Addresses = addresses;
            return this;
        }

        public IndividualCustomerBuilder SalesOrders(List<SalesOrder> salesOrders)
        {
            individualCustomer.SalesOrders = salesOrders;
            return this;
        }

        public IndividualCustomerBuilder Person(Person person)
        {
            individualCustomer.Person = person;
            return this;
        }

        public IndividualCustomer Build()
        {
            return individualCustomer;
        }

        public IndividualCustomerBuilder WithTestValues()
        {
            individualCustomer = new IndividualCustomer
            {
                AccountNumber = "AW00011000",
                Territory = "Australia",
                Person = new PersonBuilder()
                    .WithTestValues()
                    .Build(),
                Addresses = new List<CustomerAddress>
                {
                    new CustomerAddressBuilder()
                        .WithTestValues()
                        .Build()
                }
            };

            return this;
        }
    }
}
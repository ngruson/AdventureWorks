using AW.Services.Customer.Core.Handlers.GetCustomer;
using System.Collections.Generic;

namespace AW.Services.Customer.REST.API.UnitTests.TestBuilders.GetCustomer
{
    public class IndividualCustomerBuilder
    {
        private IndividualCustomerDto individualCustomer = new IndividualCustomerDto();

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

        public IndividualCustomerBuilder Addresses(List<CustomerAddressDto> addresses)
        {
            individualCustomer.Addresses = addresses;
            return this;
        }

        public IndividualCustomerBuilder SalesOrders(List<SalesOrderDto> salesOrders)
        {
            individualCustomer.SalesOrders = salesOrders;
            return this;
        }

        public IndividualCustomerBuilder Person(PersonDto person)
        {
            individualCustomer.Person = person;
            return this;
        }

        public IndividualCustomerDto Build()
        {
            return individualCustomer;
        }

        public IndividualCustomerBuilder WithTestValues()
        {
            individualCustomer = new IndividualCustomerDto
            {
                AccountNumber = "AW00011000",
                Territory = "Australia",
                Person = new PersonBuilder()
                    .WithTestValues()
                    .Build(),
                Addresses = new List<CustomerAddressDto>
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
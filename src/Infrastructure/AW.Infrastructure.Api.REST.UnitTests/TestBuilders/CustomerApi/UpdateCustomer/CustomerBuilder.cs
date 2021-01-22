using AW.Core.Abstractions.Api.CustomerApi.UpdateCustomer;

namespace AW.Infrastructure.Api.REST.UnitTests.TestBuilders.CustomerApi.UpdateCustomer
{
    public class CustomerBuilder
    {
        private Customer customer = new Customer();

        public CustomerBuilder AccountNumber(string accountNumber)
        {
            customer.AccountNumber = accountNumber;
            return this;
        }

        public CustomerBuilder Person(Person person)
        {
            customer.Person = person;
            return this;
        }

        public CustomerBuilder Store(Store store)
        {
            customer.Store = store;
            return this;
        }

        public CustomerBuilder SalesTerritoryName(string salesTerritoryName)
        {
            customer.SalesTerritoryName = salesTerritoryName;
            return this;
        }

        public Customer Build()
        {
            return customer;
        }

        public CustomerBuilder WithTestValues()
        {
            customer = new Customer
            {
                AccountNumber = "AW00000001",
                Store = new StoreBuilder().WithTestValues().Build(),
                SalesTerritoryName = "Northwest"
            };

            return this;
        }
    }
}
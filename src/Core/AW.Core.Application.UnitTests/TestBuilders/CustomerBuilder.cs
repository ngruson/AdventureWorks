using AW.Core.Domain.Person;
using AW.Core.Domain.Sales;

namespace AW.Core.Application.UnitTests.TestBuilders
{
    public class CustomerBuilder
    {
        private Domain.Sales.Customer customer = new Domain.Sales.Customer();

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

        public CustomerBuilder SalesTerritory(Domain.Sales.SalesTerritory salesTerritory)
        {
            customer.SalesTerritory = salesTerritory;
            return this;
        }

        public Domain.Sales.Customer Build()
        {
            return customer;
        }

        public CustomerBuilder WithTestValues()
        {
            customer = new Domain.Sales.Customer
            {
                AccountNumber = "AW00000001",
                Store = new StoreBuilder().WithTestValues().Build(),
                SalesTerritory = new SalesTerritoryBuilder().WithTestValues().Build()
            };

            return this;
        }
    }
}
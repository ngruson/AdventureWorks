using AW.Core.Abstractions.Api.CustomerApi.ListCustomers;
using System.Linq;

namespace AW.UI.Web.Internal.UnitTests.TestBuilders.ListCustomers
{
    public class CustomerBuilder
    {
        private Customer customer = new Customer();

        public CustomerBuilder AccountNumber(string accountNumber)
        {
            customer.AccountNumber = accountNumber;
            return this;
        }

        public CustomerBuilder SalesTerritoryName(string salesTerritoryName)
        {
            customer.SalesTerritoryName = salesTerritoryName;
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

        public CustomerBuilder SalesOrders(params SalesOrder[] salesOrders)
        {
            customer.SalesOrders = salesOrders.ToList();
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
                AccountNumber = "AW00000001"
            };

            return this;
        }
    }
}
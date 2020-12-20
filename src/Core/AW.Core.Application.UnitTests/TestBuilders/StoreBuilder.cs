using System.Collections.Generic;

namespace AW.Core.Application.UnitTests.TestBuilders
{
    public class StoreBuilder
    {
        private Domain.Sales.Store store = new Domain.Sales.Store();

        public StoreBuilder Name(string name)
        {
            store.Name = name;
            return this;
        }

        public StoreBuilder SalesPerson(Domain.Sales.SalesPerson salesPerson)
        {
            store.SalesPerson = salesPerson;
            return this;
        }

        public Domain.Sales.Store Build()
        {
            return store;
        }

        public StoreBuilder WithTestValues()
        {
            store = new Domain.Sales.Store
            {
                Name = "A Bike Store",
                SalesPerson = new SalesPersonBuilder().WithTestValues().Build(),
                BusinessEntityAddresses = new List<Domain.Person.BusinessEntityAddress>
                {
                    new BusinessEntityAddressBuilder().WithTestValues().Build()
                },
                BusinessEntityContacts = new List<Domain.Person.BusinessEntityContact>
                {
                    new BusinessEntityContactBuilder().WithTestValues().Build()
                }
            };

            return this;
        }
    }
}
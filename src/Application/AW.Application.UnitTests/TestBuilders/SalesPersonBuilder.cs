using System;

namespace AW.Application.UnitTests.TestBuilders
{
    public class SalesPersonBuilder
    {
        private Domain.Sales.SalesPerson salesPerson = new Domain.Sales.SalesPerson();

        public SalesPersonBuilder FirstName(string firstName)
        {
            salesPerson.FirstName = firstName;
            return this;
        }

        public SalesPersonBuilder MiddleName(string middleName)
        {
            salesPerson.MiddleName = middleName;
            return this;
        }

        public SalesPersonBuilder LastName(string lastName)
        {
            salesPerson.LastName = lastName;
            return this;
        }

        public Domain.Sales.SalesPerson Build()
        {
            return salesPerson;
        }

        public SalesPersonBuilder WithTestValues()
        {
            salesPerson = new Domain.Sales.SalesPerson
            {
                Id = new Random().Next(),
                FirstName = "Pamela",
                MiddleName = "O",
                LastName = "Ansman-Wolfe",
                SalesTerritory = new SalesTerritoryBuilder().WithTestValues().Build()
            };

            return this;
        }
    }
}
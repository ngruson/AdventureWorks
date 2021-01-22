using AW.Core.Abstractions.Api.CustomerApi.GetCustomer;

namespace AW.Infrastructure.Api.REST.UnitTests.TestBuilders.CustomerApi.GetCustomer
{
    public class SalesPersonBuilder
    {
        private SalesPerson salesPerson = new SalesPerson();

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

        public SalesPerson Build()
        {
            return salesPerson;
        }

        public SalesPersonBuilder WithTestValues()
        {
            salesPerson = new SalesPerson
            {
                FirstName = "Pamela",
                MiddleName = "O",
                LastName = "Ansman-Wolfe"
            };

            return this;
        }
    }
}
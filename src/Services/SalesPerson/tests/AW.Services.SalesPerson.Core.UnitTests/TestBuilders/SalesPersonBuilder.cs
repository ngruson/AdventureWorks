using System.Collections.Generic;

namespace AW.Services.SalesPerson.Core.UnitTests.TestBuilders
{
    public class SalesPersonBuilder
    {
        private Entities.SalesPerson salesPerson = new();

        public SalesPersonBuilder Title(string title)
        {
            salesPerson.Title = title;
            return this;
        }

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

        public SalesPersonBuilder Suffix(string suffix)
        {
            salesPerson.Suffix = suffix;
            return this;
        }

        public SalesPersonBuilder Territory(string territory)
        {
            salesPerson.Territory = territory;
            return this;
        }

        public Entities.SalesPerson Build()
        {
            return salesPerson;
        }

        public SalesPersonBuilder WithTestValues()
        {
            salesPerson = new Entities.SalesPerson
            {
                Title = null,
                FirstName = "Stephen",
                MiddleName = "Y",
                LastName = "Jiang",
                Suffix = null,
                Territory = null,
                EmailAddresses = new List<Entities.SalesPersonEmailAddress>
                {
                    new EmailAddressBuilder().WithTestValues().Build()
                },
                PhoneNumbers = new List<Entities.SalesPersonPhone>
                {
                    new PhoneNumberBuilder().WithTestValues().Build()
                }
            };

            return this;
        }
    }
}
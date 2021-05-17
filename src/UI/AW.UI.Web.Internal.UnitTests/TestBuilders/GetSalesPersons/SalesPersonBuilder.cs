using AW.UI.Web.Common.ApiClients.SalesPersonApi.Models;
using System.Collections.Generic;

namespace AW.UI.Web.Internal.UnitTests.TestBuilders.GetSalesPersons
{
    public class SalesPersonBuilder
    {
        private SalesPerson salesPerson = new SalesPerson();

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

        public SalesPersonBuilder EmailAddresses(List<SalesPersonEmailAddress> emailAddresses)
        {
            salesPerson.EmailAddresses = emailAddresses;
            return this;
        }

        public SalesPersonBuilder PhoneNumbers(List<SalesPersonPhone> phoneNumbers)
        {
            salesPerson.PhoneNumbers = phoneNumbers;
            return this;
        }

        public SalesPersonBuilder WithTestValues()
        {
            salesPerson = new SalesPerson
            {
                FirstName = "Stephen",
                MiddleName = "Y",
                LastName = "Jiang",
                Territory = "Northwest"
            };

            return this;
        }

        public SalesPerson Build()
        {
            return salesPerson;
        }
    }
}
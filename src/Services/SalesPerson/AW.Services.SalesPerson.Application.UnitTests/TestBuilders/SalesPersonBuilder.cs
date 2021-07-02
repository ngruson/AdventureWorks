﻿using System.Collections.Generic;

namespace AW.Services.SalesPerson.Application.UnitTests.TestBuilders
{
    public class SalesPersonBuilder
    {
        private Domain.SalesPerson salesPerson = new();

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

        public Domain.SalesPerson Build()
        {
            return salesPerson;
        }

        public SalesPersonBuilder WithTestValues()
        {
            salesPerson = new Domain.SalesPerson
            {
                Title = null,
                FirstName = "Stephen",
                MiddleName = "Y",
                LastName = "Jiang",
                Suffix = null,
                Territory = null,
                EmailAddresses = new List<Domain.SalesPersonEmailAddress>
                {
                    new EmailAddressBuilder().WithTestValues().Build()
                },
                PhoneNumbers = new List<Domain.SalesPersonPhone>
                {
                    new PhoneNumberBuilder().WithTestValues().Build()
                }
            };

            return this;
        }
    }
}
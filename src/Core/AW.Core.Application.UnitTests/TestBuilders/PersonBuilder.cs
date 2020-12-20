using AW.Core.Domain.Person;
using System;
using System.Collections.Generic;

namespace AW.Core.Application.UnitTests.TestBuilders
{
    public class PersonBuilder
    {
        private Person person = new Person();

        public PersonBuilder Title(string title)
        {
            person.Title = title;
            return this;
        }

        public PersonBuilder FirstName(string firstName)
        {
            person.FirstName = firstName;
            return this;
        }

        public PersonBuilder MiddleName(string middleName)
        {
            person.MiddleName = middleName;
            return this;
        }

        public PersonBuilder LastName(string lastName)
        {
            person.LastName = lastName;
            return this;
        }

        public PersonBuilder Suffix(string suffix)
        {
            person.Suffix = suffix;
            return this;
        }

        public Person Build()
        {
            return person;
        }

        public PersonBuilder WithTestValues()
        {
            person = new Person
            {
                Id = new Random().Next(),
                Title = "Mr.",
                FirstName = "Orlando",
                MiddleName = "N.",
                LastName = "Gee",
                BusinessEntityAddresses = new List<BusinessEntityAddress>
                {
                    new BusinessEntityAddressBuilder().WithTestValues().Build()
                },
                PhoneNumbers = new List<PersonPhone>
                {
                    new PersonPhoneBuilder().WithTestValues().Build()
                },
                EmailAddresses = new List<EmailAddress>
                {
                    new EmailAddressBuilder().WithTestValues().Build()
                }
            };

            return this;
        }
    }
}
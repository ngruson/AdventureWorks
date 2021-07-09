using AW.Services.Customer.Core.Entities;
using System.Collections.Generic;

namespace AW.Services.Customer.Core.UnitTests.TestBuilders
{
    public class PersonBuilder
    {
        private Person person = new();

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

        public PersonBuilder EmailAddresses(List<PersonEmailAddress> emailAddresses)
        {
            person.EmailAddresses = emailAddresses;
            return this;
        }

        public PersonBuilder PhoneNumbers(List<PersonPhone> phoneNumbers)
        {
            person.PhoneNumbers = phoneNumbers;
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
                FirstName = "Jon",
                MiddleName = "V",
                LastName = "Yang",
                EmailAddresses = new List<PersonEmailAddress>
                {
                    new EmailAddressBuilder()
                    .WithTestValues()
                    .Build()
                },
                PhoneNumbers = new List<PersonPhone>
                {
                    new PersonPhoneBuilder()
                        .WithTestValues()
                        .Build()
                }
            };

            return this;
        }
    }
}
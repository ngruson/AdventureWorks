using AW.Services.Customer.Application.GetCustomers;
using System.Collections.Generic;

namespace AW.Services.Customer.REST.API.UnitTests.TestBuilders.GetCustomers
{
    public class PersonBuilder
    {
        private PersonDto person = new PersonDto();

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

        public PersonBuilder EmailAddresses(List<PersonEmailAddressDto> emailAddresses)
        {
            person.EmailAddresses = emailAddresses;
            return this;
        }

        public PersonBuilder PhoneNumbers(List<PersonPhoneDto> phoneNumbers)
        {
            person.PhoneNumbers = phoneNumbers;
            return this;
        }

        public PersonDto Build()
        {
            return person;
        }

        public PersonBuilder WithTestValues()
        {
            person = new PersonDto
            {
                FirstName = "Jon",
                MiddleName = "V",
                LastName = "Yang",
                EmailAddresses = new List<PersonEmailAddressDto>
                {
                    new EmailAddressBuilder()
                    .WithTestValues()
                    .Build()
                },
                PhoneNumbers = new List<PersonPhoneDto>
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
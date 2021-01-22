using AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using System;
using System.Collections.Generic;

namespace AW.Infrastructure.Api.REST.UnitTests.TestBuilders.CustomerApi.GetCustomer
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

        public PersonBuilder Addresses(List<CustomerAddress> addresses)
        {
            person.Addresses = addresses;
            return this;
        }

        public PersonBuilder ContactInfo(List<ContactInfo> contactInfo)
        {
            person.ContactInfo = contactInfo;
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
                Title = "Mr.",
                FirstName = "Orlando",
                MiddleName = "N.",
                LastName = "Gee",
                Addresses = new List<CustomerAddress>
                {
                    new CustomerAddressBuilder().WithTestValues().Build()
                },
                ContactInfo = new List<ContactInfo>
                {
                    new ContactInfoBuilder().Build()
                }
            };

            return this;
        }
    }
}
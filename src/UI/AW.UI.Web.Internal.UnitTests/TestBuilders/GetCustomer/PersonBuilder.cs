using AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using System.Collections.Generic;
using System.Linq;

namespace AW.UI.Web.Internal.UnitTests.TestBuilders.GetCustomer
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

        public PersonBuilder FullName(string fullName)
        {
            person.FullName = fullName;
            return this;
        }

        public PersonBuilder Suffix(string suffix)
        {
            person.Suffix = suffix;
            return this;
        }

        public PersonBuilder EmailPromotion(Core.Domain.Person.EmailPromotion emailPromotion)
        {
            person.EmailPromotion = emailPromotion;
            return this;
        }

        public PersonBuilder Addresses(params CustomerAddress[] addresses)
        {
            person.Addresses = addresses.ToList();
            return this;
        }

        public PersonBuilder ContactInfo(params ContactInfo[] contactInfo)
        {
            person.ContactInfo = contactInfo.ToList();
            return this;
        }

        public Person Build()
        {
            return person;
        }
    }
}
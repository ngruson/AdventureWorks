using AW.Services.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AW.Services.Customer.Core.Entities
{
    public class Person
    {
        public Person(string title, NameFactory name)
        {
            Title = title;
            Name = name;
        }
        private Person() { }

        private int Id { get; set; }
        public string Title { get; private set; }
        //public string FirstName { get; private set; }
        //public string MiddleName { get; private set; }
        //public string LastName { get; private set; }

        public NameFactory Name { get; private set; }
        public string Suffix { get; private set; }

        public IEnumerable<PersonEmailAddress> EmailAddresses => _emailAddresses.ToList();
        private List<PersonEmailAddress> _emailAddresses = new();

        public IEnumerable<PersonPhone> PhoneNumbers => _phoneNumbers.ToList();
        private List<PersonPhone> _phoneNumbers = new();

        public void AddPhoneNumber(PersonPhone phone)
        {
            _phoneNumbers.Add(phone);
        }

        public void RemovePhoneNumber(PersonPhone phone)
        {
            _phoneNumbers.Remove(phone);
        }

        public void AddEmailAddress(PersonEmailAddress emailAddress)
        {
            _emailAddresses.Add(emailAddress);
        }

        public void RemoveEmailAddress(PersonEmailAddress emailAddress)
        {
            _emailAddresses.Remove(emailAddress);
        }
    }
}
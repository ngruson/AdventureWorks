using AW.SharedKernel.ValueTypes;
using System.Collections.Generic;

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
        public int Id { get; set; }

        public string Title { get; private set; }

        public NameFactory Name { get; private set; }
        public string Suffix { get; private set; }

        public List<PersonEmailAddress> EmailAddresses { get; internal set; } = new();

        public List<PersonPhone> PhoneNumbers { get; internal set; } = new();

        public void AddPhoneNumber(PersonPhone phone)
        {
            PhoneNumbers.Add(phone);
        }

        public void RemovePhoneNumber(PersonPhone phone)
        {
            PhoneNumbers.Remove(phone);
        }

        public void AddEmailAddress(PersonEmailAddress emailAddress)
        {
            EmailAddresses.Add(emailAddress);
        }

        public void RemoveEmailAddress(PersonEmailAddress emailAddress)
        {
            EmailAddresses.Remove(emailAddress);
        }
    }
}
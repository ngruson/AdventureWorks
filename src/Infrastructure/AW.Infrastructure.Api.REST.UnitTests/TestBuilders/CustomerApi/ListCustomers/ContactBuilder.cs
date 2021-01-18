using AW.Core.Abstractions.Api.CustomerApi.ListCustomers;

namespace AW.Infrastructure.Api.REST.UnitTests.TestBuilders.CustomerApi.ListCustomers
{
    public class ContactBuilder
    {
        private Contact contact = new Contact();

        public ContactBuilder Title(string title)
        {
            contact.Title = title;
            return this;
        }

        public ContactBuilder FirstName(string firstName)
        {
            contact.FirstName = firstName;
            return this;
        }

        public ContactBuilder MiddleName(string middleName)
        {
            contact.MiddleName = middleName;
            return this;
        }

        public ContactBuilder LastName(string lastName)
        {
            contact.LastName = lastName;
            return this;
        }

        public ContactBuilder Suffix(string suffix)
        {
            contact.Suffix = suffix;
            return this;
        }

        public Contact Build()
        {
            return contact;
        }

        public ContactBuilder WithTestValues()
        {
            contact = new Contact
            {
                Title = "Mr.",
                FirstName = "Orlando",
                MiddleName = "N.",
                LastName = "Gee"
            };

            return this;
        }
    }
}
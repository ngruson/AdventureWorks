using AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerContact;

namespace AW.Infrastructure.Api.REST.UnitTests.TestBuilders.CustomerApi.DeleteCustomerContact
{
    public class ContactBuilder
    {
        private Contact contact = new Contact();

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

        public Contact Build()
        {
            return contact;
        }

        public ContactBuilder WithTestValues()
        {
            contact = new Contact
            {
                FirstName = "Orlando",
                MiddleName = "N.",
                LastName = "Gee"
            };

            return this;
        }
    }
}
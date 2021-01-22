using AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerContact;
using System.Collections.Generic;

namespace AW.Infrastructure.Api.REST.UnitTests.TestBuilders.CustomerApi.UpdateCustomerContact
{
    public class ContactBuilder
    {
        private Contact contact = new Contact();

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
                LastName = "Gee",
                EmailAddresses = new List<EmailAddress>
                {
                    new EmailAddress { EmailAddress1 = "orlando0@adventure-works.com" }
                }
            };

            return this;
        }
    }
}
using AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerContact;

namespace AW.Infrastructure.Api.REST.UnitTests.TestBuilders.CustomerApi.UpdateCustomerContact
{
    public class CustomerContactBuilder
    {
        private CustomerContact customerContact = new CustomerContact();

        public CustomerContactBuilder ContactType(string contactType)
        {
            customerContact.ContactType = contactType;
            return this;
        }

        public CustomerContactBuilder Contact(Contact contact)
        {
            customerContact.Contact = contact;
            return this;
        }

        public CustomerContact Build()
        {
            return customerContact;
        }

        public CustomerContactBuilder WithTestValues()
        {
            customerContact = new CustomerContact
            {
                ContactType = "Owner",
                Contact = new ContactBuilder().WithTestValues().Build()
            };

            return this;
        }
    }
}
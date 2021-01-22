using AW.Core.Abstractions.Api.CustomerApi.GetCustomer;

namespace AW.Infrastructure.Api.REST.UnitTests.TestBuilders.CustomerApi.GetCustomer
{
    public class CustomerContactBuilder
    {
        private CustomerContact customerContact = new CustomerContact();

        public CustomerContactBuilder Contact(Contact contact)
        {
            customerContact.Contact = contact;
            return this;
        }

        public CustomerContactBuilder ContactType(string contactType)
        {
            customerContact.ContactType = contactType;
            return this;
        }

        public CustomerContactBuilder EmailAddress(string emailAddress)
        {
            customerContact.EmailAddress = emailAddress;
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
                Contact = new ContactBuilder().WithTestValues().Build(),
                ContactType = "Owner"
            };

            return this;
        }
    }
}
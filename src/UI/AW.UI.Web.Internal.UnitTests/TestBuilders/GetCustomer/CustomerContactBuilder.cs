using AW.Core.Abstractions.Api.CustomerApi.GetCustomer;

namespace AW.UI.Web.Internal.UnitTests.TestBuilders.GetCustomer
{
    public class CustomerContactBuilder
    {
        private CustomerContact customerContact = new CustomerContact();

        public CustomerContactBuilder ContactTypeName(string contactTypeName)
        {
            customerContact.ContactType = contactTypeName;
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
    }
}
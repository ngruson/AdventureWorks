using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetContactTypes;

namespace AW.UI.Web.Infrastructure.UnitTests.TestBuilders.GetContactTypes
{
    public class ContactTypeBuilder
    {
        private ContactType contactType = new ContactType();

        public ContactTypeBuilder Name(string name)
        {
            contactType.Name = name;
            return this;
        }

        public ContactType Build()
        {
            return contactType;
        }
    }
}
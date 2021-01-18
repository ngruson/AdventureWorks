using AW.Core.Abstractions.Api.CustomerApi.ListCustomers;

namespace AW.Infrastructure.Api.REST.UnitTests.TestBuilders.CustomerApi.ListCustomers
{
    public class ContactInfoBuilder
    {
        private ContactInfo contactInfo = new ContactInfo();

        public ContactInfo Build()
        {
            return contactInfo;
        }

        public ContactInfoBuilder WithTestValues()
        {
            contactInfo = new ContactInfo
            {
                Channel = Core.Domain.Person.ContactInfoChannelType.Phone,
                ContactInfoType = "Cell",
                Value = "245-555-0173"
            };

            return this;
        }
    }
}
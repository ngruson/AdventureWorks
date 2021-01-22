using AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using AW.Core.Domain.Person;

namespace AW.Infrastructure.Api.REST.UnitTests.TestBuilders.CustomerApi.GetCustomer
{
    public class ContactInfoBuilder
    {
        private ContactInfo contactInfo = new ContactInfo();

        public ContactInfoBuilder Channel(ContactInfoChannelType channel)
        {
            contactInfo.Channel = channel;
            return this;
        }

        public ContactInfoBuilder ContactInfoType(string contactInfoType)
        {
            contactInfo.ContactInfoType = contactInfoType;
            return this;
        }

        public ContactInfoBuilder Value(string value)
        {
            contactInfo.Value = value;
            return this;
        }

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
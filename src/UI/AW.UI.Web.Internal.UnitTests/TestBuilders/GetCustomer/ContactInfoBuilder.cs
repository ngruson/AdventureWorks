using AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using AW.Core.Domain.Person;

namespace AW.UI.Web.Internal.UnitTests.TestBuilders.GetCustomer
{
    public class ContactInfoBuilder
    {
        private ContactInfo contactInfo = new ContactInfo();

        public ContactInfoBuilder ContactInfoChannelType(ContactInfoChannelType channelType)
        {
            contactInfo.Channel = channelType;
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
    }
}
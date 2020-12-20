using AW.Core.Domain.Person;

namespace AW.Core.Abstractions.Api.CustomerApi.UpdateCustomer
{
    public class ContactInfo
    {
        public ContactInfoChannelType Channel { get; set; }
        public string ContactInfoType { get; set; }
        public string Value { get; set; }
    }
}
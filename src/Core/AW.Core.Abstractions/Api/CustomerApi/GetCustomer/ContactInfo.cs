using AW.Core.Domain.Person;

namespace AW.Core.Abstractions.Api.CustomerApi.GetCustomer
{
    public class ContactInfo
    {
        public ContactInfoChannelType Channel { get; set; }
        public string ContactInfoType { get; set; }
        public string Value { get; set; }
    }
}
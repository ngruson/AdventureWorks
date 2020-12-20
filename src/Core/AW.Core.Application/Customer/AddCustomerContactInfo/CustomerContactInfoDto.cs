namespace AW.Core.Application.Customer.AddCustomerContactInfo
{
    public class CustomerContactInfoDto
    {
        public ContactInfoChannelTypeDto Channel { get; set; }
        public string ContactInfoType { get; set; }
        public string Value { get; set; }
    }
}
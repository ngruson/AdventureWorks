namespace AW.Core.Abstractions.Api.CustomerApi.AddCustomerContactInfo
{
    public class CustomerContactInfo
    {
        public Channel Channel { get; set; }
        public string ContactInfoType { get; set; }
        public string Value { get; set; }
    }
}
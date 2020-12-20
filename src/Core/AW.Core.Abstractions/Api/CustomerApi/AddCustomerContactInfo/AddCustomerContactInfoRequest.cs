namespace AW.Core.Abstractions.Api.CustomerApi.AddCustomerContactInfo
{
    public class AddCustomerContactInfoRequest
    {
        public string AccountNumber { get; set; }
        public CustomerContactInfo CustomerContactInfo { get; set; }
    }
}
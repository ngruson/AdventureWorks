namespace AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerContactInfo
{
    public class DeleteCustomerContactInfoRequest
    {
        public string AccountNumber { get; set; }
        public CustomerContactInfo CustomerContactInfo { get; set; }
    }
}
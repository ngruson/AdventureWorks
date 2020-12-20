namespace AW.Core.Abstractions.Api.CustomerApi.AddCustomerContact
{
    public class AddCustomerContactRequest
    {
        public string AccountNumber { get; set; }
        public CustomerContact CustomerContact { get; set; }
    }
}
namespace AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerContact
{
    public class UpdateCustomerContactRequest
    {
        public string AccountNumber { get; set; }
        public CustomerContact CustomerContact { get; set; }
    }
}
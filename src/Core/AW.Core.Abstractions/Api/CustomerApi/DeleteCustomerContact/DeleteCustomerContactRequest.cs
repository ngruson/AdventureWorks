namespace AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerContact
{
    public class DeleteCustomerContactRequest
    {
        public string AccountNumber { get; set; }
        public string ContactType { get; set; }
        public Contact Contact { get; set; }
    }
}
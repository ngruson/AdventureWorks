namespace AW.Core.Abstractions.Api.CustomerApi.AddCustomerAddress
{
    public class AddCustomerAddressRequest
    {
        public string AccountNumber { get; set; }
        public CustomerAddress CustomerAddress { get; set; }
    }
}
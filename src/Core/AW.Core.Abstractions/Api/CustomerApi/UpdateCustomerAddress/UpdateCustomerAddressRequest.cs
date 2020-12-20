namespace AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerAddress
{
    public class UpdateCustomerAddressRequest
    {
        public string AccountNumber { get; set; }
        public CustomerAddress CustomerAddress { get; set; }
    }
}
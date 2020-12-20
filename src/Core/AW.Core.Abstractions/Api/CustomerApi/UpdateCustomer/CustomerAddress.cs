namespace AW.Core.Abstractions.Api.CustomerApi.UpdateCustomer
{
    public class CustomerAddress
    {
        public string AddressType { get; set; }
        public Address Address { get; set; }
    }
}
namespace AW.Core.Abstractions.Api.CustomerApi.ListCustomers
{
    public class CustomerAddress
    {
        public string AddressType { get; set; }
        public Address Address { get; set; }
    }
}
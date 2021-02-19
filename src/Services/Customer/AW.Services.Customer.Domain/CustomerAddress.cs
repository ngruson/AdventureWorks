namespace AW.Services.Customer.Domain
{
    public class CustomerAddress
    {
        public int Id { get; set; }
        public string AddressType { get; set; }
        public Address Address { get; set; }
    }
}
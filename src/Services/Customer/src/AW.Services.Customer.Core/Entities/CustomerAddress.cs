namespace AW.Services.Customer.Core.Entities
{
    public class CustomerAddress
    {
        public int Id { get; set; }
        public string AddressType { get; set; }
        public Address Address { get; set; }
        public int AddressID { get; set; }
    }
}
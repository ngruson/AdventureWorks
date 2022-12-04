namespace AW.Services.Customer.Core.Entities
{
    public class CustomerAddress
    {
        private CustomerAddress()
        {
        }
        public CustomerAddress(string addressType, Address address)
        {
            AddressType = addressType;
            Address = address;
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string AddressType { get; private set; }
        public Address Address { get; set; }
    }
}
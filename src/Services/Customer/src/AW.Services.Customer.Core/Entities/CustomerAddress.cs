namespace AW.Services.Customer.Core.Entities
{
    public class CustomerAddress
    {
        private CustomerAddress() { }
        
        public CustomerAddress(Guid objectId, string addressType, Address address) : this(addressType, address)
        {
            ObjectId = objectId;
        }
        public CustomerAddress(string addressType, Address address)
        {
            AddressType = addressType;
            Address = address;
        }

        public int Id { get; set; }
        public Guid ObjectId { get; set; }
        public int CustomerId { get; set; }
        public string? AddressType { get; private set; }
        public Address? Address { get; set; }
    }
}

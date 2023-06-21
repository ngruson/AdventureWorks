namespace AW.Services.Customer.Core.Handlers.CreateCustomer
{
    public abstract class CreatedCustomer
    {
        public Guid ObjectId { get; set; }
        public string? AccountNumber { get; set; }
        public string? Territory { get; set; }
        public List<CustomerAddress> Addresses { get; set; } = new();
    }
}

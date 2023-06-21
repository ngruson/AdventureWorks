namespace AW.Services.Customer.Core.Handlers.CreateCustomer
{
    public abstract class Customer
    {
        public string? AccountNumber { get; set; }
        public string? Territory { get; set; }
        public List<CustomerAddress> Addresses { get; set; } = new();
    }
}

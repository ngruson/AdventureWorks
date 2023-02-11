namespace AW.Services.Customer.Core.Handlers.GetAllCustomers
{
    public abstract class CustomerDto
    {
        public abstract CustomerType CustomerType { get; }
        public string? AccountNumber { get; set; }
    }
}
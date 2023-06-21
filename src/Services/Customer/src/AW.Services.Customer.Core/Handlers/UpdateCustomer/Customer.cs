using AW.SharedKernel.Interfaces;

namespace AW.Services.Customer.Core.Handlers.UpdateCustomer
{
    public abstract class Customer : ICustomer
    {
        public Guid ObjectId { get; set; }
        public string? AccountNumber { get; set; }
        public abstract CustomerType CustomerType { get; set; }
        public string? Territory { get; set; }
        public List<CustomerAddress> Addresses { get; set; } = new();
    }
}

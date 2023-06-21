using AW.SharedKernel.Interfaces;

namespace AW.Services.Customer.Core.Handlers.GetCustomer
{
    public abstract class Customer : ICustomer
    {
        public Guid ObjectId { get; set; }
        public abstract CustomerType CustomerType { get; set; }
        public string? AccountNumber { get; set; }
        public string? Territory { get; set; }
        public List<CustomerAddress>? Addresses { get; set; }
        public List<SalesOrder>? SalesOrders { get; set; }
    }
}

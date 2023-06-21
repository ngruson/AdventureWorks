using AW.SharedKernel.Interfaces;

namespace AW.Services.Customer.Core.Handlers.GetCustomers;

public abstract class Customer : ICustomer
{
    public Guid ObjectId { get; set; }
    public abstract CustomerType CustomerType { get; set; }
    public string? AccountNumber { get; set; }
}

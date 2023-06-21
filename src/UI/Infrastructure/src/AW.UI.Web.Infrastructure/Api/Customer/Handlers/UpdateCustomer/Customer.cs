using AW.SharedKernel.Interfaces;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.UpdateCustomer;

public abstract class Customer : ICustomer
{
    protected Customer() { }

    protected Customer(Guid objectId)
    {
        ObjectId = objectId;
    }

    public Guid ObjectId { get; set; }
    public string? AccountNumber { get; set; }
    public CustomerType CustomerType { get; set; }
    public string? Territory { get; set; }
    public List<CustomerAddress?>? Addresses { get; set; }
}

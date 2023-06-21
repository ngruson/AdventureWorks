namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetCustomer;

public class CustomerAddress
{
    public CustomerAddress() { }
    public CustomerAddress(Guid objectId)
    {
        ObjectId = objectId;
    }

    public Guid ObjectId { get; set; }
    public string? AddressType { get; set; }
    public Address? Address { get; set; }
}

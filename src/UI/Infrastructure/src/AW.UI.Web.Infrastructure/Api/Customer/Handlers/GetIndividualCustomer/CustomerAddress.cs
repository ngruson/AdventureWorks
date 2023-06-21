namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetIndividualCustomer;

public class CustomerAddress
{
    public Guid ObjectId { get; set; }
    public string? AddressType { get; set; }
    public Address? Address { get; set; }
}

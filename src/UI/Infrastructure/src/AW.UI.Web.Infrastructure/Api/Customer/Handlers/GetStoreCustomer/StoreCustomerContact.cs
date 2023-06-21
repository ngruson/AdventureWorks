namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetStoreCustomer;

public class StoreCustomerContact
{
    public StoreCustomerContact() { }
    public StoreCustomerContact(Guid objectId)
    {
        ObjectId = objectId;
    }

    public Guid ObjectId { get; set; }
    public string? ContactType { get; set; }
    public Person? ContactPerson { get; set; }
}

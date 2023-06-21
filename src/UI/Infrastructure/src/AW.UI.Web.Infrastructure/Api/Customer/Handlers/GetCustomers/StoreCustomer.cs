namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetCustomers;

public class StoreCustomer : Customer
{
    public string? Name { get; set; }
    public override string? CustomerName => Name;
    public string? SalesPerson { get; set; }
    public List<StoreCustomerContact> Contacts { get; set; } = new List<StoreCustomerContact>();
}

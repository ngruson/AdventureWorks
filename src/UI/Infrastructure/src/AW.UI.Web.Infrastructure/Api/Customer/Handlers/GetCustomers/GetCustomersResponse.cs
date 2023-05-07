namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetCustomers
{
    public class GetCustomersResponse
    {
        public List<Customer>? Customers { get; set; }
        public int TotalCustomers { get; set; }
    }
}
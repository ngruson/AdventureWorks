namespace AW.UI.Web.SharedKernel.Customer.Handlers.GetCustomers
{
    public class GetCustomersResponse
    {
        public List<Customer>? Customers { get; set; }
        public int TotalCustomers { get; set; }
    }
}
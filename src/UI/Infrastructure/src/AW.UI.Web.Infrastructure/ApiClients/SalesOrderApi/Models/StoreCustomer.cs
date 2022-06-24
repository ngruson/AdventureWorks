namespace AW.UI.Web.Infrastructure.ApiClients.SalesOrderApi.Models
{
    public class StoreCustomer : Customer
    {
        public string Name { get; set; }
        public override string CustomerName => Name;
    }
}
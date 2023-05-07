namespace AW.UI.Web.Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder
{
    public class StoreCustomer : Customer
    {
        public string? Name { get; set; }
        public override string? CustomerName => Name;
    }
}
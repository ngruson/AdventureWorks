namespace AW.UI.Web.SharedKernel.SalesOrder.Handlers.GetSalesOrder
{
    public class StoreCustomer : Customer
    {
        public string? Name { get; set; }
        public override string? CustomerName => Name;
    }
}
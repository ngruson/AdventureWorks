namespace AW.UI.Web.SharedKernel.SalesOrder.Handlers.GetSalesOrders
{
    public class SalesOrdersResult
    {
        public List<SalesOrder>? SalesOrders { get; set; }
        public int TotalSalesOrders { get; set; }
    }
}
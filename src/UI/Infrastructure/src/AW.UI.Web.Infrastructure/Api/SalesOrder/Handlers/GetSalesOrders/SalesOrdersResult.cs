namespace AW.UI.Web.Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders
{
    public class SalesOrdersResult
    {
        public List<SalesOrder>? SalesOrders { get; set; }
        public int TotalSalesOrders { get; set; }
    }
}
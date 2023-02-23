namespace AW.Services.Sales.Core.Models
{
    public class SalesOrdersResult //: IMapFrom<Handlers.GetSalesOrders.GetSalesOrdersDto>
    {
        public List<SalesOrder>? SalesOrders { get; set; }
        public int? TotalSalesOrders { get; set; }
    }
}

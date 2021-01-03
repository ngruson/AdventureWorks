namespace AW.Core.Abstractions.Api.SalesOrderApi.ListSalesOrders
{
    public class ListSalesOrdersRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Territory { get; set; }
        public CustomerType? CustomerType { get; set; }
    }
}
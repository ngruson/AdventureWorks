using System.Collections.Generic;

namespace AW.Core.Abstractions.Api.SalesOrderApi.ListSalesOrders
{
    public class ListSalesOrdersResponse
    {
        public int TotalSalesOrders { get; set; }
        public List<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();
    }
}
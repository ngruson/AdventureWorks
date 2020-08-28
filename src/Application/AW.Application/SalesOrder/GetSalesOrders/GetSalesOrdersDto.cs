using System.Collections.Generic;

namespace AW.Application.SalesOrder.GetSalesOrders
{
    public class GetSalesOrdersDto
    {
        public IEnumerable<SalesOrderDto> SalesOrders { get; set; }
        public int TotalSalesOrders { get; set; }
    }
}
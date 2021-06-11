using System.Collections.Generic;

namespace AW.Services.SalesOrder.Application.GetSalesOrders
{
    public class GetSalesOrdersDto
    {
        public List<SalesOrderDto> SalesOrders { get; set; }
        public int TotalSalesOrders { get; set; }
    }
}
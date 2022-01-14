using System.Collections.Generic;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrdersForCustomer
{
    public class GetSalesOrdersDto
    {
        public List<SalesOrderDto> SalesOrders { get; set; }
        public int TotalSalesOrders { get; set; }
    }
}
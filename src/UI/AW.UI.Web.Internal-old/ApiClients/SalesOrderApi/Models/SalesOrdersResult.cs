using System.Collections.Generic;

namespace AW.UI.Web.Internal.ApiClients.SalesOrderApi.Models
{
    public class SalesOrdersResult
    {
        public List<SalesOrder> SalesOrders { get; set; }
        public int TotalSalesOrders { get; set; }
    }
}
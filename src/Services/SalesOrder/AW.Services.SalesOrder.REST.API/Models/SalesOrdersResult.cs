using AW.Common.AutoMapper;
using AW.Services.SalesOrder.Application.GetSalesOrders;
using System.Collections.Generic;

namespace AW.Services.SalesOrder.REST.API.Models
{
    public class SalesOrdersResult : IMapFrom<GetSalesOrdersDto>
    {
        public List<SalesOrder> SalesOrders { get; set; }
        public int TotalSalesOrders { get; set; }
    }
}
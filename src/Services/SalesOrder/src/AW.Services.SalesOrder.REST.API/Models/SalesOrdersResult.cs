using AW.Services.SalesOrder.Core.Handlers.GetSalesOrders;
using AW.SharedKernel.AutoMapper;
using System.Collections.Generic;

namespace AW.Services.SalesOrder.REST.API.Models
{
    public class SalesOrdersResult : IMapFrom<GetSalesOrdersDto>
    {
        public List<SalesOrder> SalesOrders { get; set; }
        public int TotalSalesOrders { get; set; }
    }
}
using AutoMapper;
using AW.Services.SalesOrder.Core.Handlers.GetSalesOrders;
using AW.SharedKernel.AutoMapper;
using System.Collections.Generic;

namespace AW.Services.SalesOrder.Core.Models
{
    public class SalesOrdersResult : IMapFrom<GetSalesOrdersDto>
    {
        public List<SalesOrder> SalesOrders { get; set; }
        public int TotalSalesOrders { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetSalesOrdersDto, SalesOrdersResult>();
        }
    }
}
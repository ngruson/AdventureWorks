using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Models
{
    public class SalesOrdersResult : IMapFrom<Handlers.GetSalesOrders.GetSalesOrdersDto>
    {
        public List<SalesOrder>? SalesOrders { get; set; }
        public int? TotalSalesOrders { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Handlers.GetSalesOrders.GetSalesOrdersDto, SalesOrdersResult>();
            profile.CreateMap<Handlers.GetSalesOrdersForCustomer.GetSalesOrdersDto, SalesOrdersResult>();
        }
    }
}
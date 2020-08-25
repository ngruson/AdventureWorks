using AutoMapper;
using AW.Application.GetSalesOrders;
using AW.SalesOrderService.Messages;

namespace AW.SalesOrderService
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<GetSalesOrdersDto, ListSalesOrdersResponse>()
                .ForMember(m => m.SalesOrders, opt => opt.MapFrom(src => src));

            CreateMap<GetSalesOrdersDto, ListSalesOrders>()
                .ForMember(m => m.SalesOrder, opt => opt.MapFrom(src => src.SalesOrders));
        }
    }
}
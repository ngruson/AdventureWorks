using AutoMapper;
using ListSalesOrders = AW.Core.Abstractions.Api.SalesOrderApi.ListSalesOrders;
using GetSalesOrder = AW.Core.Abstractions.Api.SalesOrderApi.GetSalesOrder;

namespace AW.Infrastructure.Api.WCF.AutoMapper
{
    public class SalesOrderProfile : Profile
    {
        public SalesOrderProfile()
        {
            //Mappings for ListSalesOrders
            CreateMap<ListSalesOrders.ListSalesOrdersRequest, SalesOrderService.ListSalesOrdersRequest>()
                .ForMember(m => m.CustomerTypeSpecified, opt => opt.MapFrom(src => src.CustomerType.HasValue));
            CreateMap<SalesOrderService.ListSalesOrdersResponseListSalesOrdersResult, ListSalesOrders.ListSalesOrdersResponse>();
            CreateMap<SalesOrderService.SalesOrder, ListSalesOrders.SalesOrder>();
            CreateMap<SalesOrderService.Address, ListSalesOrders.Address>();
            CreateMap<SalesOrderService.ShipMethod, ListSalesOrders.ShipMethod>();
            CreateMap<SalesOrderService.SalesOrderLine, ListSalesOrders.SalesOrderLine>();
            CreateMap<SalesOrderService.SalesReason, ListSalesOrders.SalesReason>();

            //Mappings for GetProduct
            CreateMap<GetSalesOrder.GetSalesOrderRequest, SalesOrderService.GetSalesOrderRequest>();
            CreateMap<SalesOrderService.GetSalesOrderResponseGetSalesOrderResult, GetSalesOrder.GetSalesOrderResponse>();
            CreateMap<SalesOrderService.SalesOrder1, GetSalesOrder.SalesOrder>();
            CreateMap<SalesOrderService.Address1, GetSalesOrder.Address>();
            CreateMap<SalesOrderService.ShipMethod1, GetSalesOrder.ShipMethod>();
            CreateMap<SalesOrderService.SalesOrderLine1, GetSalesOrder.SalesOrderLine>();
            CreateMap<SalesOrderService.SalesReason1, GetSalesOrder.SalesReason>();
        }
    }
}
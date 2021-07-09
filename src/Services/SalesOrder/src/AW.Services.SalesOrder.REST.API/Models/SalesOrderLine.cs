using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.SalesOrder.REST.API.Models
{
    public class SalesOrderLine : IMapFrom<Core.Handlers.GetSalesOrders.SalesOrderLineDto>
    {
        public string CarrierTrackingNumber { get; set; }
        public short OrderQty { get; set; }
        public string ProductName { get; set; }
        public string SpecialOfferDescription { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitPriceDiscount { get; set; }
        public decimal LineTotal { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Handlers.GetSalesOrders.SalesOrderLineDto, SalesOrderLine>();
            profile.CreateMap<Core.Handlers.GetSalesOrder.SalesOrderLineDto, SalesOrderLine>();
        }
    }
}
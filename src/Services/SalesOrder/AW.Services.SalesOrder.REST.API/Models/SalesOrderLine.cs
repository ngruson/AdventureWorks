using AutoMapper;
using AW.Common.AutoMapper;

namespace AW.Services.SalesOrder.REST.API.Models
{
    public class SalesOrderLine : IMapFrom<Application.GetSalesOrders.SalesOrderLineDto>
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
            profile.CreateMap<Application.GetSalesOrders.SalesOrderLineDto, SalesOrderLine>();
            profile.CreateMap<Application.GetSalesOrder.SalesOrderLineDto, SalesOrderLine>();
        }
    }
}
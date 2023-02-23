using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Models
{
    public class SalesOrderLine : IMapFrom<Handlers.GetSalesOrders.SalesOrderLineDto>
    {
        public string? CarrierTrackingNumber { get; set; }
        public short OrderQty { get; set; }
        public string? ProductNumber { get; set; }
        public string? ProductName { get; set; }
        public string? SpecialOfferDescription { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitPriceDiscount { get; set; }
        public decimal LineTotal { get; set; }
        public byte[]? ThumbNailPhoto { get; set; }
        public string? Color { get; set; }
        public string? ProductLine { get; set; }
        public string? Class { get; set; }
        public string? Style { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Handlers.GetSalesOrders.SalesOrderLineDto, SalesOrderLine>();
            profile.CreateMap<Handlers.GetSalesOrder.SalesOrderLineDto, SalesOrderLine>();
            profile.CreateMap<SalesOrderLine, Handlers.UpdateSalesOrder.SalesOrderLineDto>()
                .ReverseMap();
        }
    }
}

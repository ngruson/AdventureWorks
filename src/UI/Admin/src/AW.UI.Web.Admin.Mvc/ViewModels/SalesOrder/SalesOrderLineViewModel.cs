using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.SalesOrder
{
    public class SalesOrderLineViewModel : IMapFrom<Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders.SalesOrderLine>
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
            profile.CreateMap<Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders.SalesOrderLine, SalesOrderLineViewModel>()
                .ForMember(m => m.CarrierTrackingNumber, opt => opt.MapFrom(src =>
                    !string.IsNullOrEmpty(src.CarrierTrackingNumber) ? src.CarrierTrackingNumber : "-"))
                .ForMember(m => m.ThumbNailPhoto, opt => opt.Ignore())
                .ForMember(m => m.Color, opt => opt.Ignore())
                .ForMember(m => m.ProductLine, opt => opt.Ignore())
                .ForMember(m => m.Class, opt => opt.Ignore())
                .ForMember(m => m.Style, opt => opt.Ignore());

            profile.CreateMap<Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder.SalesOrderLine, SalesOrderLineViewModel>()
                .ForMember(m => m.CarrierTrackingNumber, opt => opt.MapFrom(src =>
                    !string.IsNullOrEmpty(src.CarrierTrackingNumber) ? src.CarrierTrackingNumber : "-"));
        }
    }
}

using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.SalesOrder
{
    public class SalesOrderLineViewModel : IMapFrom<SharedKernel.SalesOrder.Handlers.GetSalesOrders.SalesOrderLine>
    {
        public string CarrierTrackingNumber { get; set; }
        public int OrderQty { get; set; }
        public string ProductName { get; set; }
        public string SpecialOfferDescription { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitPriceDiscount { get; set; }
        public decimal LineTotal { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SharedKernel.SalesOrder.Handlers.GetSalesOrders.SalesOrderLine, SalesOrderLineViewModel>()
                .ForMember(m => m.CarrierTrackingNumber, opt => opt.MapFrom(src =>
                    !string.IsNullOrEmpty(src.CarrierTrackingNumber) ? src.CarrierTrackingNumber : "-"));

            profile.CreateMap<SharedKernel.SalesOrder.Handlers.GetSalesOrder.SalesOrderLine, SalesOrderLineViewModel>()
                .ForMember(m => m.CarrierTrackingNumber, opt => opt.MapFrom(src =>
                    !string.IsNullOrEmpty(src.CarrierTrackingNumber) ? src.CarrierTrackingNumber : "-"));
        }
    }
}
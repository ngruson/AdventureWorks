using AutoMapper;
using AW.Application.AutoMapper;

namespace AW.UI.Web.Internal.ViewModels.SalesOrder
{
    public class SalesOrderLineViewModel : IMapFrom<SalesOrderService.SalesOrderLine>
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
            profile.CreateMap<SalesOrderService.SalesOrderLine, SalesOrderLineViewModel>();
            profile.CreateMap<SalesOrderService.SalesOrderLine1, SalesOrderLineViewModel>()
                .ForMember(m => m.CarrierTrackingNumber, opt => opt.MapFrom(src =>
                    !string.IsNullOrEmpty(CarrierTrackingNumber) ? src.CarrierTrackingNumber : "-"));
        }
    }
}
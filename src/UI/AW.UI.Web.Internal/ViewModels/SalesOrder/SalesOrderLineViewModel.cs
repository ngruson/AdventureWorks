using AutoMapper;
using AW.Core.Application.AutoMapper;

namespace AW.UI.Web.Internal.ViewModels.SalesOrder
{
    public class SalesOrderLineViewModel : IMapFrom<Infrastructure.Api.WCF.SalesOrderService.SalesOrderLine>
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
            profile.CreateMap<Infrastructure.Api.WCF.SalesOrderService.SalesOrderLine, SalesOrderLineViewModel>();
            profile.CreateMap<Infrastructure.Api.WCF.SalesOrderService.SalesOrderLine1, SalesOrderLineViewModel>()
                .ForMember(m => m.CarrierTrackingNumber, opt => opt.MapFrom(src =>
                    !string.IsNullOrEmpty(CarrierTrackingNumber) ? src.CarrierTrackingNumber : "-"));
        }
    }
}
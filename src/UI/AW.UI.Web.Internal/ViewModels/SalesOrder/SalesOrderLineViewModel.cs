using AutoMapper;
using AW.Common.AutoMapper;
using s = AW.UI.Web.Common.ApiClients.SalesOrderApi.Models;

namespace AW.UI.Web.Internal.ViewModels.SalesOrder
{
    public class SalesOrderLineViewModel : IMapFrom<s.SalesOrderLine>
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
            profile.CreateMap<s.SalesOrderLine, SalesOrderLineViewModel>()
                .ForMember(m => m.CarrierTrackingNumber, opt => opt.MapFrom(src =>
                    !string.IsNullOrEmpty(CarrierTrackingNumber) ? src.CarrierTrackingNumber : "-"));
        }
    }
}
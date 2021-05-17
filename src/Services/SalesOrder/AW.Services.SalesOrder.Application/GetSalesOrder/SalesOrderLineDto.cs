using AutoMapper;
using AW.Common.AutoMapper;
using AW.Services.SalesOrder.Domain;

namespace AW.Services.SalesOrder.Application.GetSalesOrder
{
    public class SalesOrderLineDto : IMapFrom<SalesOrderLine>
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
            profile.CreateMap<SalesOrderLine, SalesOrderLineDto>()
                .ForMember(m => m.SpecialOfferDescription, opt => opt.MapFrom(src => src.SpecialOfferProduct.SpecialOffer.Description));
        }
    }
}
using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Domain.Sales;

namespace AW.Core.Application.SalesOrder.GetSalesOrders
{
    public class SalesOrderLineDto : IMapFrom<SalesOrderDetail>
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
            profile.CreateMap<SalesOrderDetail, SalesOrderLineDto>()
                .ForMember(m => m.ProductName, opt => opt.MapFrom(src => src.SpecialOfferProduct.Product.Name))
                .ForMember(m => m.SpecialOfferDescription, opt => opt.MapFrom(src => src.SpecialOfferProduct.SpecialOffer.Description));
        }
    }
}
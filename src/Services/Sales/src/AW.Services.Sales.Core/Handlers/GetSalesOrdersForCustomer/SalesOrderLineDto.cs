using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrdersForCustomer
{
    public class SalesOrderLineDto : IMapFrom<Entities.SalesOrderLine>
    {
        public string CarrierTrackingNumber { get; set; }

        public short OrderQty { get; set; }
        public string ProductNumber { get; set; }

        public string ProductName { get; set; }

        public string SpecialOfferDescription { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal UnitPriceDiscount { get; set; }

        public decimal LineTotal { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.SalesOrderLine, SalesOrderLineDto>()
                .ForMember(m => m.SpecialOfferDescription, opt => opt.MapFrom(src => src.SpecialOfferProduct.SpecialOffer.Description));
        }
    }
}
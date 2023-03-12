using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrder
{
    public class SalesOrderLine : IMapFrom<Entities.SalesOrderLine>
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
            profile.CreateMap<Entities.SalesOrderLine, SalesOrderLine>()
                .ForMember(m => m.UnitPrice, opt => opt.MapFrom(src => Math.Round(src.UnitPrice, 2)))
                .ForMember(m => m.UnitPriceDiscount, opt => opt.MapFrom(src => Math.Round(src.UnitPriceDiscount, 2)))
                .ForMember(m => m.SpecialOfferDescription, opt => opt.MapFrom(src => src.SpecialOffer!.Description));
        }
    }
}

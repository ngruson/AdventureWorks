using System.Text.Json.Serialization;
using Ardalis.SmartEnum.SystemTextJson;
using AutoMapper;
using AW.Services.Sales.Core.Entities;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Handlers.UpdateSalesOrder
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
        public byte[]? ThumbNailPhoto { get; set; }
        public string? Color { get; set; }

        [JsonConverter(typeof(SmartEnumNameConverter<ProductLine, string>))]
        public ProductLine? ProductLine { get; set; }

        [JsonConverter(typeof(SmartEnumNameConverter<Class, string>))]
        public Class? Class { get; set; }

        [JsonConverter(typeof(SmartEnumNameConverter<Style, string>))]
        public Style? Style { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.SalesOrderLine, SalesOrderLine>()
                .ForMember(m => m.SpecialOfferDescription, opt => opt.MapFrom(src => src.SpecialOffer!.Description))
                .ReverseMap();
        }
    }
}

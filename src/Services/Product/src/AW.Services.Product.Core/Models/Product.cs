using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Product.Core.Models
{
    public class Product : IMapFrom<Handlers.GetProduct.Product>
    {
        public string? Name { get; set; }
        public string? ProductNumber { get; set; }
        public string? Color { get; set; }
        public decimal ListPrice { get; set; }
        public string? Size { get; set; }
        public string? SizeUnitMeasureCode { get; set; }
        public decimal Weight { get; set; }
        public string? WeightUnitMeasureCode { get; set; }
        public string? ProductLine { get; set; }
        public string? Class { get; set; }
        public string? Style { get; set; }
        public string? ProductSubcategoryName { get; set; }
        public string? ProductCategoryName { get; set; }
        public byte[]? ThumbnailPhoto { get; set; }
        public byte[]? LargePhoto { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Handlers.GetProduct.Product, Product>()
                .ForMember(m => m.WeightUnitMeasureCode, opt => opt.MapFrom(src => src.WeightUnitMeasureCode!.Trim()))
                .ForMember(m => m.ProductLine, opt => opt.MapFrom(src => src.ProductLine!.Trim()))
                .ForMember(m => m.Style, opt => opt.MapFrom(src => src.Style!.Trim()));
        }
    }
}
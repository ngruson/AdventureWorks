using AutoMapper;
using AW.Services.Product.Core.Entities;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Product.Core.Handlers.CreateProduct
{
    public class Product : IMapFrom<Entities.Product>
    {
        public string? Name { get; set; }
        public string? ProductNumber { get; set; }
        public bool? MakeFlag { get; set; }
        public bool? FinishedGoodsFlag { get; set; }
        public string? Color { get; set; }
        public short SafetyStockLevel { get; set; }
        public short ReorderPoint { get; set; }
        public decimal StandardCost { get; set; }
        public decimal ListPrice { get; set; }
        public string? Size { get; set; }
        public string? SizeUnitMeasureCode { get; set; }
        public decimal Weight { get; set; }
        public string? WeightUnitMeasureCode { get; set; }
        public int DaysToManufacture { get; set; }
        public string? ProductLine { get; set; }
        public string? Class { get; set; }
        public string? Style { get; set; }
        public DateTime SellStartDate { get; set; }

        public DateTime? SellEndDate { get; set; }

        public DateTime? DiscontinuedDate { get; set; }
        public string? ProductModelName { get; set; }
        public string? ProductSubcategoryName { get; set; }
        public string? ProductCategoryName { get; set; }
        public List<ProductProductPhoto>? ProductProductPhotos { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, Entities.Product>()
                .ForMember(_ => _.Id, opt => opt.Ignore())
                .ForMember(_ => _.ProductSubcategoryId, opt => opt.Ignore())
                .ForMember(_ => _.ProductModelId, opt => opt.Ignore())
                .ForMember(_ => _.ProductModel, opt => opt.Ignore())
                .ForMember(_ => _.ProductSubcategory, opt => opt.Ignore())
                .ForMember(_ => _.SizeUnitMeasure, opt => opt.Ignore())
                .ForMember(_ => _.WeightUnitMeasure, opt => opt.Ignore())
                .ForMember(_ => _.ProductLine, opt => opt.MapFrom(src => MapProductLine(src.ProductLine)))
                .ForMember(_ => _.Class, opt => opt.MapFrom(src => MapClass(src.Class)))
                .ForMember(_ => _.Style, opt => opt.MapFrom(src => MapStyle(src.Style)))
                .ForMember(m => m.SizeUnitMeasureCode, opt => opt.MapFrom(src => src.SizeUnitMeasureCode!.Trim()))
                .ForMember(m => m.WeightUnitMeasureCode, opt => opt.MapFrom(src => src.WeightUnitMeasureCode!.Trim()))
                .ReverseMap();
        }

        private static ProductLine? MapProductLine(string? productLine)
        {
            if (!string.IsNullOrEmpty(productLine))
                return Entities.ProductLine.FromName(productLine, false);
            
            return null;
        }

        private static Class? MapClass(string? @class)
        {
            if (!string.IsNullOrEmpty(@class))
                return Entities.Class.FromName(@class, false);

            return null;
        }

        private static Style? MapStyle(string? style)
        {
            if (!string.IsNullOrEmpty(style))
                return Entities.Style.FromName(style, false);

            return null;
        }
    }
}

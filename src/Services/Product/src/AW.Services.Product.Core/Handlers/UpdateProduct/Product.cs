using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Product.Core.Handlers.UpdateProduct
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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Product, Product>()
                .ReverseMap()
                .ForMember(m => m.ProductModel, opt => opt.Ignore())
                .ForMember(m => m.ProductLine, opt => opt.MapFrom(src => MapProductLine(src.ProductLine)))
                .ForMember(m => m.Class, opt => opt.MapFrom(src => MapClass(src.Class)))
                .ForMember(m => m.Style, opt => opt.MapFrom(src => MapStyle(src.Style)));
        }

        private static Entities.ProductLine? MapProductLine(string? productLine)
        {
            if (!string.IsNullOrEmpty(productLine))
                return Entities.ProductLine.FromName(productLine, false);

            return null;
        }

        private static Entities.Class? MapClass(string? @class)
        {
            if (!string.IsNullOrEmpty(@class))
                return Entities.Class.FromName(@class, false);

            return null;
        }

        private static Entities.Style? MapStyle(string? style)
        {
            if (!string.IsNullOrEmpty(style))
                return Entities.Style.FromName(style, false);

            return null;
        }
    }
}

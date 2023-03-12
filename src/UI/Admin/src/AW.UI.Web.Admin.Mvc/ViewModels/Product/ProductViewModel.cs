using System.ComponentModel.DataAnnotations;
using System.Drawing;
using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Product
{
    public class ProductViewModel : IMapFrom<SharedKernel.Product.Handlers.GetProducts.Product>
    {
        public string? Name { get; set; }
        
        [Display(Name = "Product number")]
        public string? ProductNumber { get; set; }

        [Display(Name = "Make")]
        public bool MakeFlag { get; set; }

        [Display(Name = "Finished goods")]
        public bool FinishedGoodsFlag { get; set; }

        public string? Color { get; set; }

        [Display(Name = "Safety stock level")]
        public short SafetyStockLevel { get; set; }

        [Display(Name = "Reorder point")]
        public short ReorderPoint { get; set; }

        [Display(Name = "Standard cost")]
        public decimal StandardCost { get; set; }

        [Display(Name = "List price")]
        public decimal ListPrice { get; set; }

        public string? Size { get; set; }
        public string? SizeUnitMeasureCode { get; set; }
        public string? Weight { get; set; }
        public string? WeightUnitMeasureCode { get; set; }

        [Display(Name = "Days to manufacture")]
        public int DaysToManufacture { get; set; }

        [Display(Name = "Product line")]
        public string? ProductLine { get; set; }
        public string? Class { get; set; }
        public string? Style { get; set; }

        [Display(Name = "Sell start date")]
        public DateTime SellStartDate { get; set; }

        [Display(Name = "Sell end date")]
        public DateTime? SellEndDate { get; set; }

        [Display(Name = "Discontinued date")]
        public DateTime? DiscontinuedDate { get; set; }

        [Display(Name = "Subcategory")] 
        public string? ProductSubcategoryName { get; set; }

        [Display(Name = "Category")]
        public string? ProductCategoryName { get; set; }
        public byte[]? ThumbnailPhoto { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SharedKernel.Product.Handlers.GetProducts.Product, ProductViewModel>()
                .ForMember(m => m.MakeFlag, opt => opt.Ignore())
                .ForMember(m => m.FinishedGoodsFlag, opt => opt.Ignore())
                .ForMember(m => m.Color, opt => opt.Ignore())
                .ForMember(m => m.SafetyStockLevel, opt => opt.Ignore())
                .ForMember(m => m.ReorderPoint, opt => opt.Ignore())
                .ForMember(m => m.StandardCost, opt => opt.Ignore())
                .ForMember(m => m.DaysToManufacture, opt => opt.Ignore())
                .ForMember(m => m.SellStartDate, opt => opt.Ignore())
                .ForMember(m => m.SellEndDate, opt => opt.Ignore())
                .ForMember(m => m.DiscontinuedDate, opt => opt.Ignore());

            profile.CreateMap<SharedKernel.Product.Handlers.GetProduct.Product, ProductViewModel>()
                .ForMember(m => m.SizeUnitMeasureCode, opt => opt.MapFrom(src => src.SizeUnitMeasureCode!.ToLower()))
                .ForMember(m => m.WeightUnitMeasureCode, opt => opt.MapFrom(src => src.WeightUnitMeasureCode!.ToLower()));
        }
    }
}

using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Product;

public class ProductViewModel : IMapFrom<Infrastructure.Api.Product.Handlers.GetProducts.Product>
{
    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }
    
    [Display(Name = "Product number")]
    public string? ProductNumber { get; set; }

    [Display(Name = "Make")]
    public bool MakeFlag { get; set; }

    [Display(Name = "Finished goods")]
    public bool FinishedGoodsFlag { get; set; }

    public string? Color { get; set; }

    [Display(Name = "Safety stock level")]
    public int SafetyStockLevel { get; set; }

    [Display(Name = "Reorder point")]
    public int ReorderPoint { get; set; }

    [Display(Name = "Standard cost")]
    public string? StandardCost { get; set; }

    [Display(Name = "List price")]
    public string? ListPrice { get; set; }

    public string? Size { get; set; }
    public string? SizeUnitMeasureCode { get; set; }
    public decimal? Weight { get; set; }
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

    [Display(Name = "Product model")]
    public string? ProductModelName { get; set; }

    [Display(Name = "Subcategory")] 
    public string? ProductSubcategoryName { get; set; }

    [Display(Name = "Category")]
    public string? ProductCategoryName { get; set; }

    public List<ProductProductPhotoViewModel>? ProductProductPhotos { get; set; }

    public ProductPhotoViewModel? GetPrimaryPhoto()
    {
        var photo = ProductProductPhotos?.SingleOrDefault(_ => _.Primary);
        return photo?.ProductPhoto;
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Infrastructure.Api.Product.Handlers.GetProducts.Product, ProductViewModel>()
            .ForMember(m => m.MakeFlag, opt => opt.Ignore())
            .ForMember(m => m.FinishedGoodsFlag, opt => opt.Ignore())
            .ForMember(m => m.Color, opt => opt.Ignore())
            .ForMember(m => m.SafetyStockLevel, opt => opt.Ignore())
            .ForMember(m => m.ReorderPoint, opt => opt.Ignore())
            .ForMember(m => m.StandardCost, opt => opt.Ignore())
            .ForMember(m => m.DaysToManufacture, opt => opt.Ignore())
            .ForMember(m => m.SellStartDate, opt => opt.Ignore())
            .ForMember(m => m.SellEndDate, opt => opt.Ignore())
            .ForMember(m => m.DiscontinuedDate, opt => opt.Ignore())
            .ForMember(m => m.ProductModelName, opt => opt.Ignore());

        profile.CreateMap<Infrastructure.Api.Product.Handlers.GetProduct.Product, ProductViewModel>();
    }
}

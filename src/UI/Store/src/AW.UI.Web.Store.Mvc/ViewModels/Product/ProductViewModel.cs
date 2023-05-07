using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Store.Mvc.ViewModels.Product
{
    public class ProductViewModel : IMapFrom<Infrastructure.Api.Product.Handlers.GetProducts.Product>
    {
        public string? Name { get; set; }
        public string? ProductNumber { get; set; }
        public string? Color { get; set; }
        public string? ListPrice { get; set; }
        public string? Size { get; set; }
        public string? SizeUnitMeasureCode { get; set; }
        public decimal Weight { get; set; }
        public string? WeightUnitMeasureCode { get; set; }
        public string? ProductLine { get; set; }
        public string? Class { get; set; }
        public string? Style { get; set; }
        public string? ProductSubcategoryName { get; set; }
        public string? ProductCategoryName { get; set; }
        public string? ThumbNailPhoto { get; set; }
        public string? LargePhoto { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Infrastructure.Api.Product.Handlers.GetProducts.Product, ProductViewModel>()
                .ForMember(m => m.Color, opt => opt.Ignore())
                .ForMember(m => m.ListPrice, opt => opt.MapFrom(src => src.ListPrice.ToString("C")))
                .ForMember(m => m.ThumbNailPhoto, opt => opt.MapFrom(src => src.GetPrimaryPhoto()!.ThumbNailPhoto != null ?
                    $"data:image;base64,{Convert.ToBase64String(src.GetPrimaryPhoto()!.ThumbNailPhoto!)}" : null))
                .ForMember(m => m.LargePhoto, opt => opt.MapFrom(src => src.GetPrimaryPhoto()!.LargePhoto != null ?
                    $"data:image;base64,{Convert.ToBase64String(src.GetPrimaryPhoto()!.LargePhoto!)}" : null));
        }
    }
}

using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Store.ViewModels.Product
{
    public class ProductViewModel : IMapFrom<SharedKernel.Product.Handlers.GetProducts.Product>
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
        public string? ThumbnailPhoto { get; set; }
        public string? LargePhoto { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SharedKernel.Product.Handlers.GetProducts.Product, ProductViewModel>()
                .ForMember(m => m.ListPrice, opt => opt.MapFrom(src => src.ListPrice.ToString("C")))
                .ForMember(m => m.ThumbnailPhoto, opt => opt.MapFrom(src => src.ThumbnailPhoto != null ?
                    $"data:image;base64,{Convert.ToBase64String(src.ThumbnailPhoto)}" : null))
                .ForMember(m => m.LargePhoto, opt => opt.MapFrom(src => src.LargePhoto != null ?
                    $"data:image;base64,{Convert.ToBase64String(src.LargePhoto)}" : null));
        }
    }
}
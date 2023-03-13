using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Product
{
    public class ProductPhotoViewModel : IMapFrom<SharedKernel.Product.Handlers.GetProduct.ProductPhoto>
    {
        public byte[]? ThumbNailPhoto { get; set; }

        public string? ThumbnailPhotoFileName { get; set; }

        public byte[]? LargePhoto { get; set; }

        public string? LargePhotoFileName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SharedKernel.Product.Handlers.GetProducts.ProductPhoto, ProductPhotoViewModel>();
            profile.CreateMap<SharedKernel.Product.Handlers.GetProduct.ProductPhoto, ProductPhotoViewModel>();
        }
    }
}

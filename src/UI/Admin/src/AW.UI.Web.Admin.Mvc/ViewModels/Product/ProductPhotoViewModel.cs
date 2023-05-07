using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Product
{
    public class ProductPhotoViewModel : IMapFrom<Infrastructure.Api.Product.Handlers.GetProduct.ProductPhoto>
    {
        public byte[]? ThumbNailPhoto { get; set; }

        public string? ThumbnailPhotoFileName { get; set; }

        public byte[]? LargePhoto { get; set; }

        public string? LargePhotoFileName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Infrastructure.Api.Product.Handlers.GetProducts.ProductPhoto, ProductPhotoViewModel>();
            profile.CreateMap<Infrastructure.Api.Product.Handlers.GetProduct.ProductPhoto, ProductPhotoViewModel>();
        }
    }
}

using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Product
{
    public class ProductProductPhotoViewModel : IMapFrom<SharedKernel.Product.Handlers.GetProducts.ProductProductPhoto>
    {
        public bool Primary { get; set; }

        public ProductPhotoViewModel? ProductPhoto { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SharedKernel.Product.Handlers.GetProducts.ProductProductPhoto, ProductProductPhotoViewModel>();
            profile.CreateMap<SharedKernel.Product.Handlers.GetProduct.ProductProductPhoto, ProductProductPhotoViewModel>();
        }
    }
}

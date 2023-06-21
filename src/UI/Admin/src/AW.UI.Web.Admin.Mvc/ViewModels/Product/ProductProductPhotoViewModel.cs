using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Product;

public class ProductProductPhotoViewModel : IMapFrom<Infrastructure.Api.Product.Handlers.GetProducts.ProductProductPhoto>
{
    public bool Primary { get; set; }

    public ProductPhotoViewModel? ProductPhoto { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Infrastructure.Api.Product.Handlers.GetProducts.ProductProductPhoto, ProductProductPhotoViewModel>();
        profile.CreateMap<Infrastructure.Api.Product.Handlers.GetProduct.ProductProductPhoto, ProductProductPhotoViewModel>();
    }
}

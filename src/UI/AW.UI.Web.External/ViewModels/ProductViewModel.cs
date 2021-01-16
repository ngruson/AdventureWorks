using AutoMapper;
using AW.Core.Abstractions.Api.ProductApi.ListProducts;
using AW.Core.Application.AutoMapper;

namespace AW.UI.Web.External.ViewModels
{
    public class ProductViewModel : IMapFrom<Product>
    {
        public string Name { get; set; }
        public decimal ListPrice { get; set; }
        public byte[] LargePhoto { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductViewModel>();
        }
    }
}
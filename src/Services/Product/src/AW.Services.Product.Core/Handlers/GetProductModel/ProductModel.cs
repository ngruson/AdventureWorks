using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Product.Core.Handlers.GetProductModel
{
    public class ProductModel : IMapFrom<Entities.ProductModel>
    {
        public string? Name { get; set; }

        public string? CatalogDescription { get; set; }

        public string? Instructions { get; set; }
        public List<ProductModelDescription>? Descriptions { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.ProductModel, ProductModel>()
                .ForMember(_ => _.Descriptions, opt => opt.MapFrom(src => src.ProductModelProductDescriptionCultures));
        }
    }
}

using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Product.Core.Handlers.CreateProduct
{
    public class ProductProductPhoto : IMapFrom<Entities.ProductProductPhoto>
    {
        public bool Primary { get; set; }

        public ProductPhoto? ProductPhoto { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductProductPhoto, Entities.ProductProductPhoto>()
                .ForMember(_ => _.ProductId, opt => opt.Ignore())
                .ForMember(_ => _.ProductPhotoId, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}

using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Product.Core.Handlers.CreateProduct
{
    public class ProductPhoto : IMapFrom<Entities.ProductPhoto>
    {
        public byte[]? ThumbNailPhoto { get; set; }

        public string? ThumbnailPhotoFileName { get; set; }

        public byte[]? LargePhoto { get; set; }

        public string? LargePhotoFileName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductPhoto, Entities.ProductPhoto>()
                .ForMember(_ => _.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}

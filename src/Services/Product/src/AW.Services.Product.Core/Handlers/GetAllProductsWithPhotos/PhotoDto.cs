using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Product.Core.Handlers.GetAllProductsWithPhotos
{
    public class PhotoDto : IMapFrom<Entities.ProductProductPhoto>
    {
        public byte[]? ThumbNailPhoto { get; set; }

        public string? ThumbnailPhotoFileName { get; set; }

        public byte[]? LargePhoto { get; set; }

        public string? LargePhotoFileName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.ProductProductPhoto, PhotoDto>()
                .ForMember(m => m.ThumbnailPhotoFileName, opt => opt.MapFrom(src => src.ProductPhoto!.ThumbnailPhotoFileName))
                .ForMember(m => m.ThumbNailPhoto, opt => opt.MapFrom(src => src.ProductPhoto!.ThumbNailPhoto))
                .ForMember(m => m.LargePhotoFileName, opt => opt.MapFrom(src => src.ProductPhoto!.LargePhotoFileName))
                .ForMember(m => m.LargePhoto, opt => opt.MapFrom(src => src.ProductPhoto!.LargePhoto));
        }
    }
}
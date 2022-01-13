using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System.Collections.Generic;

namespace AW.Services.Product.Core.Handlers.GetAllProductsWithPhotos
{
    public class ProductWithPhotoDto : IMapFrom<Entities.Product>
    {
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public List<PhotoDto> Photos { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Product, ProductWithPhotoDto>()
                .ForMember(_ => _.Photos, opt => opt.MapFrom(_ => _.ProductProductPhotos));
        }
    }
}
using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Product.Core.Handlers.GetProductModel
{
    public class ProductModelDescription : IMapFrom<Entities.ProductModelProductDescriptionCulture>
    {
        public string? CultureName { get; set; }
        public string? Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.ProductModelProductDescriptionCulture, ProductModelDescription>()
                .ForMember(_ => _.Description, opt => opt.MapFrom(src => src.ProductDescription!.Description));
        }
    }
}

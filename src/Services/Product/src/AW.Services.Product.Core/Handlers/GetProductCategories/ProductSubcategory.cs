using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Product.Core.Handlers.GetProductCategories
{
    public class ProductSubcategory : IMapFrom<Core.Entities.ProductSubcategory>
    {
        public string Name { get; set; }
        public int ProductCount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.ProductSubcategory, ProductSubcategory>()
                .ForMember(m => m.ProductCount, opt => opt.MapFrom(src => src.Products.Count));
        }
    }
}
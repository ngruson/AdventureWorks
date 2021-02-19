using AutoMapper;
using AW.Services.Product.Application.Common;

namespace AW.Services.Product.Application.GetProductCategories
{
    public class ProductSubcategory : IMapFrom<Domain.ProductSubcategory>
    {
        public string Name { get; set; }
        public int ProductCount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.ProductSubcategory, ProductSubcategory>()
                .ForMember(m => m.ProductCount, opt => opt.MapFrom(src => src.Products.Count));
        }
    }
}
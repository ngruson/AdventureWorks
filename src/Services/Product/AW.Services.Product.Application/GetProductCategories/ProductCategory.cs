using AutoMapper;
using AW.Services.Product.Application.Common;
using System.Collections.Generic;

namespace AW.Services.Product.Application.GetProductCategories
{
    public class ProductCategory : IMapFrom<Domain.ProductCategory>
    {
        public string Name { get; set; }
        public List<ProductSubcategory> Subcategories { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.ProductCategory, ProductCategory>()
                .ForMember(m => m.Subcategories, opt => opt.MapFrom(src => src.ProductSubcategory));
        }
    }
}
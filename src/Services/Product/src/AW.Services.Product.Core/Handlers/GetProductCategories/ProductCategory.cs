using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System.Collections.Generic;

namespace AW.Services.Product.Core.Handlers.GetProductCategories
{
    public class ProductCategory : IMapFrom<Entities.ProductCategory>
    {
        public string Name { get; set; }
        public List<ProductSubcategory> Subcategories { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.ProductCategory, ProductCategory>()
                .ForMember(m => m.Subcategories, opt => opt.MapFrom(src => src.ProductSubcategory));
        }
    }
}
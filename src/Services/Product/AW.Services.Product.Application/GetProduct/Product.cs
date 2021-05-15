using AutoMapper;
using AW.Services.Product.Application.Common.AutoMapper;
using System.Linq;

namespace AW.Services.Product.Application.GetProduct
{
    public class Product : IMapFrom<Domain.Product>
    {
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public string Color { get; set; }
        public decimal ListPrice { get; set; }
        public string Size { get; set; }
        public string SizeUnitMeasureCode { get; set; }
        public decimal Weight { get; set; }
        public string WeightUnitMeasureCode { get; set; }
        public string ProductLine { get; set; }
        public string Class { get; set; }
        public string Style { get; set; }
        public string ProductSubcategoryName { get; set; }
        public string ProductCategoryName { get; set; }
        public byte[] ThumbnailPhoto { get; set; }
        public byte[] LargePhoto { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Product, Product>()
                .ForMember(m => m.ProductCategoryName, opt => opt.MapFrom(src =>
                    src.ProductSubcategory.ProductCategory.Name))
                .ForMember(m => m.ThumbnailPhoto, opt => opt.MapFrom((src, dest) =>
                {
                    var primaryPhoto = src.ProductProductPhotos.SingleOrDefault(p => p.Primary);
                    return primaryPhoto.ProductPhoto.ThumbNailPhoto;
                }))
                .ForMember(m => m.ThumbnailPhoto, opt => opt.MapFrom((src, dest) =>
                    {
                        var primaryPhoto = src.ProductProductPhotos.SingleOrDefault(p => p.Primary);
                        if (primaryPhoto != null)
                            return primaryPhoto.ProductPhoto.ThumbNailPhoto;
                        return null;
                    })
                )
                .ForMember(m => m.LargePhoto, opt => opt.MapFrom((src, dest) =>
                    {
                        var primaryPhoto = src.ProductProductPhotos.SingleOrDefault(p => p.Primary);
                        if (primaryPhoto != null)
                            return primaryPhoto.ProductPhoto.LargePhoto;
                        return null;
                    })
                );
        }
    }
}
using AutoMapper;
using AW.Services.Product.Application.Common;

namespace AW.Services.Product.REST.API.Models
{
    public class Product : IMapFrom<Application.GetProducts.Product>
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
            profile.CreateMap<Application.GetProducts.Product, Product>();
            profile.CreateMap<Application.GetProduct.Product, Product>();
        }
    }
}
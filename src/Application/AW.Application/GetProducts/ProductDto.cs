using AutoMapper;
using AW.Application.Common.Mappings;
using AW.Domain.Production;

namespace AW.Application.GetProducts
{
    public class ProductDto : IMapFrom<Product>
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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDto>();
        }
    }
}
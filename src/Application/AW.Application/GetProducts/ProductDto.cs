﻿using AutoMapper;
using AW.Application.Common.Mappings;
using AW.Domain.Production;
using System.Linq;

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
        public byte[] LargePhoto { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDto>()
                .ForMember(m => m.LargePhoto, opt => opt.MapFrom((src, dest) =>
                    {
                        var primaryPhoto = src.ProductProductPhotos.SingleOrDefault(p => p.Primary);
                        return primaryPhoto.ProductPhoto.LargePhoto;
                    }));
        }
    }
}
using AW.Common.AutoMapper;
using AW.Services.Product.Application.GetProducts;
using System.Collections.Generic;

namespace AW.Services.Product.REST.API.Models
{
    public class GetProductsResult : IMapFrom<GetProductsDto>
    {
        public List<Product> Products { get; set; }
        public int TotalProducts { get; set; }
    }
}
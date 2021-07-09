using AW.SharedKernel.AutoMapper;
using AW.Services.Product.Core.Handlers.GetProducts;
using System.Collections.Generic;

namespace AW.Services.Product.REST.API.Models
{
    public class GetProductsResult : IMapFrom<GetProductsDto>
    {
        public List<Product> Products { get; set; }
        public int TotalProducts { get; set; }
    }
}
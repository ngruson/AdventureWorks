using AW.SharedKernel.AutoMapper;
using AW.Services.Product.Core.Handlers.GetProducts;

namespace AW.Services.Product.REST.API.Models
{
    public class GetProductsResult : IMapFrom<GetProductsDto>
    {
        public List<Core.Models.Product>? Products { get; set; }
        public int TotalProducts { get; set; }
    }
}
using AW.Application.GetProducts;
using MediatR;

namespace AW.Application.GetProduct
{
    public class GetProductQuery : IRequest<ProductDto>
    {
        public string ProductNumber { get; set; }
    }
}
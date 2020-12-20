using MediatR;

namespace AW.Core.Application.Product.GetProduct
{
    public class GetProductQuery : IRequest<ProductDto>
    {
        public string ProductNumber { get; set; }
    }
}
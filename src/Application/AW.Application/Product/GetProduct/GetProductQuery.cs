using MediatR;

namespace AW.Application.Product.GetProduct
{
    public class GetProductQuery : IRequest<ProductDto>
    {
        public string ProductNumber { get; set; }
    }
}
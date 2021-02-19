using MediatR;

namespace AW.Services.Product.Application.GetProduct
{
    public class GetProductQuery : IRequest<Product>
    {
        public string ProductNumber { get; set; }
    }
}
using MediatR;

namespace AW.Services.Product.Core.Handlers.GetProduct
{
    public class GetProductQuery : IRequest<Product>
    {
        public string ProductNumber { get; set; }
    }
}
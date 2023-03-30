using MediatR;

namespace AW.Services.Product.Core.Handlers.CreateProduct
{
    public class CreateProductCommand : IRequest<Product>
    {
        public Product? Product { get; set; }
    }
}

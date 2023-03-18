using MediatR;

namespace AW.Services.Product.Core.Handlers.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Product>
    {
        public UpdateProductCommand(Product product)
        {
            Product = product;
        }
        public Product? Product { get; set; }
    }
}

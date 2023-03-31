using MediatR;

namespace AW.Services.Product.Core.Handlers.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Product>
    {
        public UpdateProductCommand(string key, Product product)
        {
            Key = key;
            Product = product;
        }

        public string Key { get; set; }
        public Product? Product { get; set; }
    }
}

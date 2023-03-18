using MediatR;

namespace AW.UI.Web.SharedKernel.Product.Handlers.UpdateProduct
{
    public class UpdateProductCommand : IRequest
    {
        public UpdateProductCommand(Product product)
        {
            Product = product;
        }
        public Product? Product { get; set; }
    }
}

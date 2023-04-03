using MediatR;

namespace AW.UI.Web.SharedKernel.Product.Handlers.CreateProduct
{
    public class CreateProductCommand : IRequest
    {
        public CreateProductCommand(Product product)
        {
            Product = product;
        }

        public Product Product { get; set; }
    }
}

using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Product.Handlers.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {
        public DeleteProductCommand(string productNumber)
        {
            ProductNumber = productNumber;
        }

        public string ProductNumber { get; set; }
    }
}

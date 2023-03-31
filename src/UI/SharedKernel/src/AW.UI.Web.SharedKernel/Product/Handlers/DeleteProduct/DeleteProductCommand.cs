using MediatR;

namespace AW.UI.Web.SharedKernel.Product.Handlers.DeleteProduct
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

using MediatR;

namespace AW.UI.Web.SharedKernel.Product.Handlers.DuplicateProduct
{
    public class DuplicateProductCommand : IRequest<Product>
    {
        public DuplicateProductCommand(string productNumber)
        {
            ProductNumber = productNumber;
        }
        public string ProductNumber  { get; set; }
    }
}

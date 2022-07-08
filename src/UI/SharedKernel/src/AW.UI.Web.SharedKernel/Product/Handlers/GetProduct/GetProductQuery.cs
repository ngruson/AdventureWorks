using MediatR;

namespace AW.UI.Web.SharedKernel.Product.Handlers.GetProduct
{
    public class GetProductQuery : IRequest<Product>
    {
        public GetProductQuery(string? productNumber)
        {
            ProductNumber = productNumber;
        }

        public string? ProductNumber { get; set; }
    }
}
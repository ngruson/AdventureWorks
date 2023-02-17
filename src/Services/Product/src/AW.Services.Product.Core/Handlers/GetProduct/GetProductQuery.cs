using MediatR;

namespace AW.Services.Product.Core.Handlers.GetProduct
{
    public class GetProductQuery : IRequest<Product>
    {
        public GetProductQuery()
        {
        }
        public GetProductQuery(string productNumber)
        {
            ProductNumber = productNumber;
        }

        public string? ProductNumber { get; set; }
    }
}

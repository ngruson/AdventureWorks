using AW.Application.Product.CountProducts;
using AW.Application.Product.GetProduct;
using AW.Application.Product.GetProducts;
using AW.ProductService.Messages;
using MediatR;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.ProductService
{
    [ServiceBehavior(Namespace = "http://services.aw.com/ProductService/1.0")]
    public class ProductService : IProductService
    {
        private readonly IMediator mediator;

        public ProductService(IMediator mediator) =>
            (this.mediator) = (mediator);

        public async Task<ListProductsResponse> ListProducts(ListProductsRequest request)
        {
            var query = new GetProductsQuery
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };
            var products = await mediator.Send(query);
            
            var response = new ListProductsResponse
            {
                TotalProducts = await mediator.Send(new CountProductsQuery()),
                Products = products.ToList(),
            };

            return response;
        }

        public async Task<GetProductResponse> GetProduct(GetProductRequest request)
        {
            var query = new GetProductQuery
            {
                ProductNumber = request.ProductNumber
            };
            var product = await mediator.Send(query);

            var response = new GetProductResponse
            {
                Product = product
            };

            return response;
        }
    }
}
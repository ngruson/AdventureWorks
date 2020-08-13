using AW.Application.GetProducts;
using AW.ProductService.Messages;
using MediatR;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.ProductService
{
    [ServiceBehavior(Namespace = "http://services.aw.com/ProductService")]
    public class ProductService : IProductService
    {
        private readonly IMediator mediator;

        public ProductService(IMediator mediator) =>
            (this.mediator) = (mediator);

        public async Task<ListProductsResponse> ListProducts()
        {
            var query = new GetProductsQuery();
            var products = await mediator.Send(query);
            
            var response = new ListProductsResponse
            {
                Products = products.ToList()
            };

            return response;
        }
    }
}
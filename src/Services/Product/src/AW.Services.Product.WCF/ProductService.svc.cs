using MediatR;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using AW.Services.Product.WCF.Messages;
using AW.Services.Product.Core.Handlers.GetProducts;
using AW.Services.Product.Core.Handlers.CountProducts;
using AW.Services.Product.Core.Handlers.GetProduct;
using AutoMapper;
using System.Collections.Generic;

namespace AW.Services.Product.WCF
{
    [ServiceBehavior(Namespace = "http://services.aw.com/ProductService/1.0")]
    public class ProductService : IProductService
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public ProductService(IMediator mediator, IMapper mapper) => (this.mediator, this.mapper) = (mediator, mapper);

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
                Products = new ListProducts
                {
                    Product = mapper.Map<List<Core.Models.Product>>(products.Products)
                }
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
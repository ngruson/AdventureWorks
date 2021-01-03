using AutoMapper;
using AW.Core.Abstractions.Api.ProductApi;
using AW.Core.Abstractions.Api.ProductApi.GetProduct;
using AW.Core.Abstractions.Api.ProductApi.ListProducts;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AW.Infrastructure.Api.WCF
{
    public class ProductServiceAdapter : IProductApi
    {
        private readonly ILogger<ProductServiceAdapter> logger;
        private readonly IMapper mapper;
        private readonly ProductService.IProductService productService;

        public ProductServiceAdapter(
            ILogger<ProductServiceAdapter> logger,
            IMapper mapper,
            ProductService.IProductService productService
        ) => (this.logger, this.mapper, this.productService) = (logger, mapper, productService);

        public async Task<ListProductsResponse> ListProductsAsync(ListProductsRequest request)
        {
            logger.LogInformation("Mapping to ListProductsRequest");
            var req = mapper.Map<ProductService.ListProductsRequest>(request);

            logger.LogInformation("Calling ListProducts operation of Product web service");
            var response = await productService.ListProductsAsync(req);
            logger.LogInformation("ListProducts operation executed succesfully");

            return mapper.Map<ListProductsResponse>(response);
        }

        public async Task<GetProductResponse> GetProduct(GetProductRequest request)
        {
            logger.LogInformation("Mapping to GetProductRequest");
            var req = mapper.Map<ProductService.GetProductRequest>(request);

            logger.LogInformation("Calling GetProduct operation of Product web service");
            var response = await productService.GetProductAsync(req);
            logger.LogInformation("GetProduct operation executed succesfully");

            return mapper.Map<GetProductResponse>(response);
        }
    }
}
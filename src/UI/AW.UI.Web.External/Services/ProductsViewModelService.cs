using AutoMapper;
using AW.UI.Web.External.Interfaces;
using AW.UI.Web.External.ProductService;
using AW.UI.Web.External.ViewModels;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.UI.Web.External.Services
{
    public class ProductsViewModelService : IProductsViewModelService
    {
        private readonly ILogger<ProductsViewModelService> logger;
        private readonly IMapper mapper;
        private readonly IProductService productService;

        public ProductsViewModelService(
            ILoggerFactory loggerFactory,
            IMapper mapper,
            IProductService productService)
        {
            logger = loggerFactory.CreateLogger<ProductsViewModelService>();
            this.mapper = mapper;
            this.productService = productService;
        }

        public async Task<ProductsIndexViewModel> GetProducts()
        {
            logger.LogInformation("GetProducts called");

            var products = await productService.ListProductsAsync(new ListProductsRequest());

            var vm = new ProductsIndexViewModel
            {
                Products = mapper.Map<List<ProductViewModel>>(products.Body.ListProductsResult)
            };

            return vm;
        }
    }
}
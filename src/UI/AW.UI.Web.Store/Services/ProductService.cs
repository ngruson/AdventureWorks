using AutoMapper;
using AW.UI.Web.Store.ApiClients.ProductApi;
using AW.UI.Web.Store.ViewModels;
using AW.UI.Web.Store.ViewModels.Home;
using AW.UI.Web.Store.ViewModels.Product;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.UI.Web.Store.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductApiClient productApiClient;
        private readonly ILogger<ProductService> logger;
        private readonly IMapper mapper;

        public ProductService(IProductApiClient productApiClient, ILogger<ProductService> logger, IMapper mapper) =>
            (this.productApiClient, this.logger, this.mapper) = (productApiClient, logger, mapper);

        public async Task<HomeViewModel> GetCategories()
        {
            logger.LogInformation("GetCategories called");
            var response = await productApiClient.GetCategoriesAsync();

            var vm = new HomeViewModel
            {
                ProductCategories = response
            };

            return vm;
        }

        public async Task<ProductsViewModel> GetProducts(int pageIndex, int pageSize, string category, string subcategory)
        {
            logger.LogInformation("GetProducts called");
            var products = await productApiClient.GetProductsAsync(
                    pageIndex, pageSize, category, subcategory, "asc(productNumber)"
            );

            var vm = new ProductsViewModel
            {
                Title = subcategory ?? category,
                ProductCategory = category,
                ProductSubcategory = subcategory,
                ProductCategories = await productApiClient.GetCategoriesAsync(),
                Products = mapper.Map<List<ProductViewModel>>(products.Products),
                PaginationInfo = new PaginationInfoViewModel()
                {
                    ActualPage = pageIndex,
                    ItemsPerPage = products.Products.Count,
                    TotalItems = products.TotalProducts,
                    TotalPages = int.Parse(Math.Ceiling(((decimal)products.TotalProducts / pageSize)).ToString())
                }
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "disabled" : "";

            return vm;
        }
    }
}
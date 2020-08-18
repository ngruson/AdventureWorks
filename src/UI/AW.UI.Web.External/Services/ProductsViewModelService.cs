using AutoMapper;
using AW.UI.Web.External.Interfaces;
using AW.UI.Web.External.ProductService;
using AW.UI.Web.External.ViewModels;
using Microsoft.Extensions.Logging;
using System;
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

        public async Task<ProductsIndexViewModel> GetProducts(int pageIndex, int pageSize)
        {
            logger.LogInformation("GetProducts called");

            var response = await productService.ListProductsAsync(
                new ListProductsRequest
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize
                }
            );

            var vm = new ProductsIndexViewModel
            {
                Products = mapper.Map<List<ProductViewModel>>(response.Products),
                PaginationInfo = new PaginationInfoViewModel()
                {
                    ActualPage = pageIndex,
                    ItemsPerPage = response.Products.Length,
                    TotalItems = response.TotalProducts,
                    TotalPages = int.Parse(Math.Ceiling(((decimal)response.TotalProducts / pageSize)).ToString())
                }
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

            return vm;
        }
    }
}
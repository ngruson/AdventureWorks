using Ardalis.GuardClauses;
using AutoMapper;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Admin.Mvc.ViewModels;
using AW.UI.Web.Admin.Mvc.ViewModels.Product;
using AW.UI.Web.Admin.Mvc.ViewModels.SalesOrder;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProduct;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProducts;
using AW.UI.Web.SharedKernel.Product.Handlers.UpdateProduct;
using MediatR;

namespace AW.UI.Web.Admin.Mvc.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductService(
            ILogger<ProductService> logger,
            IMapper mapper,
            IMediator mediator
        )
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }        

        public async Task<ProductIndexViewModel> GetProducts(int pageIndex, int pageSize)
        {
            _logger.LogInformation("GetProducts called");
            var response = await _mediator.Send(new GetProductsQuery(
                    pageIndex,
                    pageSize,
                    null, 
                    null
                )
            );

            var totalPages = int.Parse(Math.Ceiling((decimal)response.TotalProducts / pageSize).ToString());

            var vm = new ProductIndexViewModel
            {
                Products = _mapper.Map<List<ProductViewModel>>(response.Products),
                PaginationInfo = new PaginationInfoViewModel(
                    response.TotalProducts,
                    response.Products!.Count,
                    pageIndex,
                    totalPages,
                    pageIndex == 0 ? "disabled" : "",
                    pageIndex == totalPages - 1 ? "disabled" : ""
                )
            };

            return vm;
        }

        private async Task<SharedKernel.Product.Handlers.GetProduct.Product> GetProduct(string? productNumber)
        {
            _logger.LogInformation("Getting product for {ProductNumber}", productNumber);
            var product = await _mediator.Send(new GetProductQuery(productNumber));
            _logger.LogInformation("Retrieved product {@Product}", product);
            Guard.Against.Null(product, _logger);

            return product!;
        }

        public async Task<ProductDetailViewModel> GetProductDetail(string productNumber)
        {
            var product = await GetProduct(productNumber);

            return new ProductDetailViewModel
            {
                Product = _mapper.Map<ProductViewModel>(product)
            };
        }

        private async Task UpdateProduct(SharedKernel.Product.Handlers.UpdateProduct.Product product)
        {
            _logger.LogInformation("Updating product {@Product}", product);
            await _mediator.Send(new UpdateProductCommand(product));
            _logger.LogInformation("Product updated successfully");
        }

        public async Task UpdateProduct(UpdateProductViewModel viewModel)
        {
            var product = await GetProduct(viewModel!.Product!.ProductNumber);
            var productToUpdate = _mapper.Map<SharedKernel.Product.Handlers.UpdateProduct.Product>(product);            
            await UpdateProduct(productToUpdate);
        }
    }
}

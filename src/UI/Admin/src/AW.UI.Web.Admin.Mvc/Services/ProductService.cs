using Ardalis.GuardClauses;
using AutoMapper;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Admin.Mvc.ViewModels;
using AW.UI.Web.Admin.Mvc.ViewModels.Product;
using DeleteProduct = AW.UI.Web.SharedKernel.Product.Handlers.DeleteProduct;
using DuplicateProduct = AW.UI.Web.SharedKernel.Product.Handlers.DuplicateProduct;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProduct;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProductCategories;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProducts;
using AW.UI.Web.SharedKernel.Product.Handlers.UpdateProduct;
using MediatR;
using AW.UI.Web.SharedKernel.Product.Handlers.CreateProduct;

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

        public async Task<ProductIndexViewModel> GetProducts()
        {
            _logger.LogInformation("Getting products");
            var response = await _mediator.Send(new GetProductsQuery(null, null));

            var vm = new ProductIndexViewModel(
                _mapper.Map<List<ProductViewModel>>(response.Products)
            );

            _logger.LogInformation("Returning {ViewModel}", vm);
            return vm;
        }

        private async Task<SharedKernel.Product.Handlers.GetProduct.Product> GetProduct(string? productNumber)
        {
            _logger.LogInformation("Getting product");
            var product = await _mediator.Send(new GetProductQuery(productNumber));
            _logger.LogInformation("Retrieved product");
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

        private async Task UpdateProduct(string key, SharedKernel.Product.Handlers.UpdateProduct.Product product)
        {
            _logger.LogInformation("Updating product");
            await _mediator.Send(new UpdateProductCommand(key, product));
            _logger.LogInformation("Product updated successfully");
        }

        public async Task AddProduct(AddProductViewModel viewModel)
        {            
            var product = _mapper.Map<SharedKernel.Product.Handlers.CreateProduct.Product>(viewModel.Product);

            _logger.LogInformation("Send command to add product");
            await _mediator.Send(new CreateProductCommand(product));
            _logger.LogInformation("Command was succesfully executed");
        }

        public async Task UpdateProduct(EditProductViewModel viewModel)
        {
            var product = await GetProduct(viewModel!.Key);
            var productToUpdate = _mapper.Map<SharedKernel.Product.Handlers.UpdateProduct.Product>(product);
            _mapper.Map(viewModel.Product, productToUpdate);

            await UpdateProduct(viewModel.Key!, productToUpdate);
        }

        public async Task UpdatePricing(EditPricingViewModel viewModel)
        {
            _logger.LogInformation("Getting product details");
            var product = await GetProduct(viewModel!.Product!.ProductNumber);
            _logger.LogInformation("Received product details");

            _logger.LogInformation(
                "Mapping {Source} to {Target}",
                viewModel.GetType().Name,
                typeof(SharedKernel.Product.Handlers.UpdateProduct.Product).Name
            );
            var productToUpdate = _mapper.Map<SharedKernel.Product.Handlers.UpdateProduct.Product>(product);
            _mapper.Map(viewModel.Product, productToUpdate);

            _logger.LogInformation("Updating product");
            await UpdateProduct(viewModel.Product.ProductNumber!, productToUpdate);
        }

        public async Task UpdateProductOrganization(EditProductOrganizationViewModel viewModel)
        {
            _logger.LogInformation("Getting product details");
            var product = await GetProduct(viewModel!.Product!.ProductNumber);
            _logger.LogInformation("Received product details");

            _logger.LogInformation(
                "Mapping {Source} to {Target}",
                viewModel.GetType().Name,
                typeof(SharedKernel.Product.Handlers.UpdateProduct.Product).Name
            );
            var productToUpdate = _mapper.Map<SharedKernel.Product.Handlers.UpdateProduct.Product>(product);
            _mapper.Map(viewModel.Product, productToUpdate);

            _logger.LogInformation("Updating product");
            await UpdateProduct(viewModel.Product.ProductNumber!, productToUpdate);
        }

        public async Task DeleteProduct(string productNumber)
        {
            _logger.LogInformation("Deleting product");
            await _mediator.Send(new DeleteProduct.DeleteProductCommand(productNumber));
            _logger.LogInformation("Product successfully deleted");
        }

        public async Task<DuplicateProduct.Product> DuplicateProduct(string productNumber)
        {
            _logger.LogInformation("Duplicating product");
            var product = await _mediator.Send(new DuplicateProduct.DuplicateProductCommand(productNumber));
            _logger.LogInformation("Product successfully duplicated");

            return product;
        }

        public async Task<ProductCategory> GetCategory(string categoryName)
        {
            var categories = await _mediator.Send(new GetProductCategoriesQuery());
            var category = categories.Single(c => c.Name == categoryName);
            return category;
        }
    }
}

using AW.UI.Web.Admin.Mvc.ViewModels.Product;
using AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProductCategories;
using AW.UI.Web.Infrastructure.Api.Product.Handlers.DuplicateProduct;

namespace AW.UI.Web.Admin.Mvc.Services;

public interface IProductService
{        
    Task AddProduct(AddProductViewModel viewModel);
    Task UpdateProduct(EditProductViewModel viewModel);
    Task UpdatePricing(EditPricingViewModel viewModel);
    Task UpdateProductOrganization(EditProductOrganizationViewModel viewModel);
    Task DeleteProduct(string productNumber);
    Task<Product> DuplicateProduct(string productNumber);
    Task<ProductIndexViewModel> GetProducts();
    Task<ProductDetailViewModel> GetProductDetail(string productNumber);
    Task<ProductCategory> GetCategory(string categoryName);
}

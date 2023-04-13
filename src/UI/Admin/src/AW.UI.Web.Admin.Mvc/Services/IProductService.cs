using AW.UI.Web.Admin.Mvc.ViewModels.Product;
using GetProductCategories = AW.UI.Web.SharedKernel.Product.Handlers.GetProductCategories;
using DuplicateProduct = AW.UI.Web.SharedKernel.Product.Handlers.DuplicateProduct;

namespace AW.UI.Web.Admin.Mvc.Services
{
    public interface IProductService
    {        
        Task AddProduct(AddProductViewModel viewModel);
        Task UpdateProduct(EditProductViewModel viewModel);
        Task UpdatePricing(EditPricingViewModel viewModel);
        Task UpdateProductOrganization(EditProductOrganizationViewModel viewModel);
        Task DeleteProduct(string productNumber);
        Task<DuplicateProduct.Product> DuplicateProduct(string productNumber);
        Task<ProductIndexViewModel> GetProducts();
        Task<ProductDetailViewModel> GetProductDetail(string productNumber);
        Task<GetProductCategories.ProductCategory> GetCategory(string categoryName);
    }
}

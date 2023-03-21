using AW.UI.Web.Admin.Mvc.ViewModels.Product;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProductCategories;

namespace AW.UI.Web.Admin.Mvc.Services
{
    public interface IProductService
    {
        Task<ProductIndexViewModel> GetProducts(int pageIndex, int pageSize);
        Task<ProductDetailViewModel> GetProductDetail(string productNumber);
        Task<ProductCategory> GetCategory(string categoryName);
        Task UpdateProduct(EditProductViewModel viewModel);
        Task UpdatePricing(EditPricingViewModel viewModel);
        Task UpdateProductOrganization(EditProductOrganizationViewModel viewModel);
    }
}

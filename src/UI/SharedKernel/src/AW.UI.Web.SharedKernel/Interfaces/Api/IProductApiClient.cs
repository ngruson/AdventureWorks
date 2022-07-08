using AW.UI.Web.SharedKernel.Product.Handlers.GetProductCategories;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProducts;

namespace AW.UI.Web.SharedKernel.Interfaces.Api
{
    public interface IProductApiClient
    {
        Task<List<ProductCategory>> GetCategoriesAsync();
        Task<GetProductsResult> GetProductsAsync(int pageIndex, int pageSize, string? category, string? subcategory, string? orderBy);
        Task<Product.Handlers.GetProduct.Product> GetProductAsync(string? productNumber);
    }
}
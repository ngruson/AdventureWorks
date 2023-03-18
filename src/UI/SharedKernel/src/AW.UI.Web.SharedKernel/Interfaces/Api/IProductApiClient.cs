using AW.UI.Web.SharedKernel.Product.Handlers.GetProductCategories;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProductModels;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProducts;
using AW.UI.Web.SharedKernel.Product.Handlers.GetUnitMeasures;

namespace AW.UI.Web.SharedKernel.Interfaces.Api
{
    public interface IProductApiClient
    {
        Task<List<ProductCategory>?> GetCategories();
        Task<GetProductsResult?> GetProducts(int pageIndex, int pageSize, string? category, string? subcategory, string? orderBy);
        Task<Product.Handlers.GetProduct.Product?> GetProduct(string? productNumber);
        Task<Product.Handlers.UpdateProduct.Product?> UpdateProduct(Product.Handlers.UpdateProduct.Product product);
        Task<List<ProductModel>?> GetProductModels();
        Task<List<UnitMeasure>?> GetUnitMeasures();
    }
}

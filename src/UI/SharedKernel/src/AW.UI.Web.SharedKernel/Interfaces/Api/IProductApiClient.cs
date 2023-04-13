using AW.UI.Web.SharedKernel.Product.Handlers.GetProductCategories;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProducts;
using AW.UI.Web.SharedKernel.Product.Handlers.GetUnitMeasures;

namespace AW.UI.Web.SharedKernel.Interfaces.Api
{
    public interface IProductApiClient
    {        
        Task<GetProductsResult?> GetProducts(string? category, string? subcategory, string? orderBy);
        Task<Product.Handlers.GetProduct.Product?> GetProduct(string? productNumber);
        Task<Product.Handlers.CreateProduct.Product?> CreateProduct(Product.Handlers.CreateProduct.Product product);
        Task<Product.Handlers.UpdateProduct.Product?> UpdateProduct(string key, Product.Handlers.UpdateProduct.Product product);
        Task DeleteProduct(string productNumber);
        Task<Product.Handlers.DuplicateProduct.Product> DuplicateProduct(string productNumber);
        Task<List<ProductCategory>?> GetCategories();
        Task<List<Product.Handlers.GetProductModels.ProductModel>?> GetProductModels();
        Task<Product.Handlers.GetProductModel.ProductModel?> GetProductModel(string name);
        Task<List<UnitMeasure>?> GetUnitMeasures();
        
    }
}

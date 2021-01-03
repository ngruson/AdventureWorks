using AW.Core.Abstractions.Api.ProductApi.GetProduct;
using AW.Core.Abstractions.Api.ProductApi.ListProducts;
using System.Threading.Tasks;

namespace AW.Core.Abstractions.Api.ProductApi
{
    public interface IProductApi
    {
        Task<ListProductsResponse> ListProductsAsync(ListProductsRequest request);
        Task<GetProductResponse> GetProduct(GetProductRequest request);
    }
}
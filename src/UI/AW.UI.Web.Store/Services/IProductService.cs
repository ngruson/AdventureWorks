using AW.UI.Web.Common.ApiClients.ProductApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.UI.Web.Store.Services
{
    public interface IProductService
    {
        Task<List<ProductCategory>> GetCategoriesAsync();
        Task<GetProductsResult> GetProductsAsync(int pageIndex, int pageSize, string category, string subcategory);
    }
}
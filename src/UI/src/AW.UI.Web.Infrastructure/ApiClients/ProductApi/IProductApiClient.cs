using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.UI.Web.Infrastructure.ApiClients.ProductApi
{
    public interface IProductApiClient
    {
        Task<List<Models.ProductCategory>> GetCategoriesAsync();
        Task<Models.GetProductsResult> GetProductsAsync(int pageIndex, int pageSize, string category, string subcategory, string orderBy);
    }
}
using AW.UI.Web.Store.ViewModels.Home;
using AW.UI.Web.Store.ViewModels.Product;
using System.Threading.Tasks;

namespace AW.UI.Web.Store.Services
{
    public interface IProductService
    {
        Task<HomeViewModel> GetCategories();
        Task<ProductsViewModel> GetProducts(int pageIndex, int pageSize, string category, string subcategory);
    }
}
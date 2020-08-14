using AW.UI.Web.External.ViewModels;
using System.Threading.Tasks;

namespace AW.UI.Web.External.Interfaces
{
    public interface IProductsViewModelService
    {
        Task<ProductsIndexViewModel> GetProducts();
    }
}
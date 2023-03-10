using AW.UI.Web.Admin.Mvc.ViewModels.Customer;
using AW.UI.Web.Admin.Mvc.ViewModels.Product;

namespace AW.UI.Web.Admin.Mvc.Services
{
    public interface IProductService
    {
        Task<ProductIndexViewModel> GetProducts(int pageIndex, int pageSize);
    }
}

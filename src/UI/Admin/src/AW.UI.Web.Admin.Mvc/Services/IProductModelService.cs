using AW.UI.Web.Admin.Mvc.ViewModels.ProductModel;

namespace AW.UI.Web.Admin.Mvc.Services
{
    public interface IProductModelService
    {
        Task<List<ProductModelViewModel>> GetProductModels();
    }
}

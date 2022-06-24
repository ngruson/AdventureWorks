using AW.UI.Web.Store.Services;
using AW.UI.Web.Store.ViewModels.ProductMenu;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AW.UI.Web.Store.ViewComponents
{
    public class ProductMenu : ViewComponent
    {
        private readonly IProductService productService;

        public ProductMenu(IProductService productService) =>
            this.productService = productService;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vm = new ProductMenuComponentViewModel
            {
                Categories = await productService.GetCategoriesAsync()
            };
            return View(vm);
        }
    }
}
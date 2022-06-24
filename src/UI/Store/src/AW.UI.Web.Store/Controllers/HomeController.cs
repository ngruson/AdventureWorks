using AW.UI.Web.Store.Services;
using AW.UI.Web.Store.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AW.UI.Web.Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService productService;

        public HomeController(IProductService productService) =>
            (this.productService) = (productService);

        public async Task<IActionResult> Index()
        {
            var vm = new HomeViewModel
            {
                ProductCategories = await productService.GetCategoriesAsync()
            };

            return View(vm);
        }
    }
}
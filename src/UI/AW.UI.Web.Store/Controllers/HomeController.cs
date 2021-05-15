using AW.UI.Web.Store.Services;
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
            return View(
                await productService.GetCategories()
            );
        }
    }
}
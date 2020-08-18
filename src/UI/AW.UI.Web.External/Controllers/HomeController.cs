using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AW.UI.Web.External.Models;
using AW.UI.Web.External.Interfaces;
using System.Threading.Tasks;

namespace AW.UI.Web.External.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsViewModelService productsViewModelService;

        public HomeController(IProductsViewModelService productsViewModelService)
        {
            this.productsViewModelService = productsViewModelService;
        }

        public async Task<IActionResult> Index(int? pageId)
        {
            return View(await productsViewModelService.GetProducts(pageId ?? 0, Constants.ITEMS_PER_PAGE));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

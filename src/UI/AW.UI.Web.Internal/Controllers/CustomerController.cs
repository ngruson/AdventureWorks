using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AW.UI.Web.Internal.Models;
using AW.UI.Web.Internal.Interfaces;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomersViewModelService customersViewModelService;

        public CustomerController(ICustomersViewModelService customersViewModelService)
        {
            this.customersViewModelService = customersViewModelService;
        }

        public async Task<IActionResult> Index(int? pageId, string territoryFilterApplied, string customerTypeFilterApplied)
        {
            return View(
                await customersViewModelService.GetCustomers(
                    pageId ?? 0, 
                    Constants.ITEMS_PER_PAGE,
                    territoryFilterApplied,
                    customerTypeFilterApplied
                )
            );
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

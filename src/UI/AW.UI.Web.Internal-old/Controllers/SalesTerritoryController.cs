using AW.UI.Web.Internal.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.Controllers
{
    public class SalesTerritoryController : Controller
    {
        private readonly ISalesTerritoryViewModelService salesTerritoryViewModelService;

        public SalesTerritoryController(ISalesTerritoryViewModelService salesTerritoryViewModelService) =>
            (this.salesTerritoryViewModelService) = salesTerritoryViewModelService;

        public async Task<IActionResult> Index()
        {
            return View(
                await salesTerritoryViewModelService.GetSalesTerritories()
            );
        }
    }
}
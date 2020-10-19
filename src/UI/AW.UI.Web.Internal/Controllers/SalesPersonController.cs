using AW.UI.Web.Internal.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.Controllers
{
    public class SalesPersonController : Controller
    {
        private readonly ISalesPersonViewModelService salesPersonViewModelService;

        public SalesPersonController(ISalesPersonViewModelService salesPersonViewModelService) =>
            (this.salesPersonViewModelService) = salesPersonViewModelService;

        public async Task<IActionResult> Index()
        {
            return View(
                await salesPersonViewModelService.GetSalesPersons(
                )
            );
        }
    }
}
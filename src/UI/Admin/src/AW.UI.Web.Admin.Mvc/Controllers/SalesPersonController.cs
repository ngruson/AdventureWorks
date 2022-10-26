using AW.UI.Web.Admin.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AW.UI.Web.Admin.Mvc.Controllers
{
    public class SalesPersonController : Controller
    {
        private readonly ISalesPersonViewModelService salesPersonViewModelService;

        public SalesPersonController(ISalesPersonViewModelService salesPersonViewModelService) =>
            this.salesPersonViewModelService = salesPersonViewModelService;

        public async Task<IActionResult> Index()
        {
            return View(
                await salesPersonViewModelService.GetSalesPersons(
                )
            );
        }
    }
}
using salesOrderApi = AW.UI.Web.Infrastructure.ApiClients.SalesOrderApi.Models;
using AW.UI.Web.Internal.Extensions;
using AW.UI.Web.Internal.Interfaces;
using AW.UI.Web.Internal.Services;
using AW.UI.Web.Internal.ViewModels.SalesOrder;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.Controllers
{
    public class SalesOrderController : Controller
    {
        private readonly ISalesOrderService salesOrderService;
        private readonly ISalesPersonViewModelService salesPersonViewModelService;
        private readonly IReferenceDataService referenceDataService;

        public SalesOrderController(
            ISalesOrderService salesOrdersViewModelService,
            ISalesPersonViewModelService salesPersonViewModelService,
            IReferenceDataService referenceDataService
        )
        {
            this.salesOrderService = salesOrdersViewModelService;
            this.salesPersonViewModelService = salesPersonViewModelService;
            this.referenceDataService = referenceDataService;
        }

        public async Task<IActionResult> Index(int? pageId, string territoryFilterApplied, string customerTypeFilterApplied)
        {
            return View(
                await salesOrderService.GetSalesOrders(
                    pageId ?? 0,
                    Constants.ITEMS_PER_PAGE,
                    territoryFilterApplied,
                    !string.IsNullOrEmpty(customerTypeFilterApplied) ? Enum.Parse<salesOrderApi.CustomerType>(customerTypeFilterApplied) : default(salesOrderApi.CustomerType?)
                )
            );
        }

        public async Task<IActionResult> Detail(string salesOrderNumber)
        {
            return View(
                await salesOrderService.GetSalesOrder(
                    salesOrderNumber)
            );
        }

        public async Task<IActionResult> ApproveSalesOrder(string salesOrderNumber)
        {
            var salesOrder = await salesOrderService.GetSalesOrderForApproval(
                salesOrderNumber
            );

            ViewData["territories"] = 
                (await referenceDataService.GetTerritoriesAsync(
                    salesOrder.BillToAddress.CountryRegionCode
                ))
                .OrderBy(c => c.Name)
                .ToList()
                .ToSelectList(c => c.Name, c => c.Name);

            ViewData["salesPersons"] =
                (await salesPersonViewModelService.GetSalesPersons(
                    salesOrder.Territory
                ))
                .SalesPersons
                .OrderBy(s => s.Name.FullName)
                .ToList()
                .ToSelectList(sp => sp.Name.FullName, sp => sp.Name.FullName);

            return View(salesOrder);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveSalesOrder(ApproveSalesOrderViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await salesOrderService.UpdateSalesOrder(viewModel);
            await salesOrderService.ApproveSalesOrder(viewModel.SalesOrderNumber);

            return RedirectToAction(
                nameof(Detail), 
                new { salesOrderNumber = viewModel.SalesOrderNumber }
            );
        }

        public async Task<JsonResult> GetSalesPersons(string territory)
        {
            var result = await salesPersonViewModelService.GetSalesPersons(territory);
            return Json(result.SalesPersons);
        }
    }
}
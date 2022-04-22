using AW.UI.Web.Infrastructure.ApiClients.SalesOrderApi.Models;
using AW.UI.Web.Infrastructure.ApiClients.SalesPersonApi.Models;
using AW.UI.Web.Internal.Extensions;
using AW.UI.Web.Internal.Interfaces;
using AW.UI.Web.Internal.Services;
using AW.UI.Web.Internal.ViewModels.SalesOrder;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.Controllers
{
    public class SalesOrderController : Controller
    {
        private readonly ISalesOrderViewModelService salesOrderViewModelService;
        private readonly ISalesPersonViewModelService salesPersonViewModelService;
        private readonly IReferenceDataService referenceDataService;

        public SalesOrderController(
            ISalesOrderViewModelService salesOrdersViewModelService,
            ISalesPersonViewModelService salesPersonViewModelService,
            IReferenceDataService referenceDataService
        )
        {
            this.salesOrderViewModelService = salesOrdersViewModelService;
            this.salesPersonViewModelService = salesPersonViewModelService;
            this.referenceDataService = referenceDataService;
        }

        public async Task<IActionResult> Index(int? pageId, string territoryFilterApplied, string customerTypeFilterApplied)
        {
            return View(
                await salesOrderViewModelService.GetSalesOrders(
                    pageId ?? 0,
                    Constants.ITEMS_PER_PAGE,
                    territoryFilterApplied,
                    !string.IsNullOrEmpty(customerTypeFilterApplied) ? Enum.Parse<CustomerType>(customerTypeFilterApplied) : default(CustomerType?)
                )
            );
        }

        public async Task<IActionResult> Detail(string salesOrderNumber)
        {
            return View(
                await salesOrderViewModelService.GetSalesOrder(
                    salesOrderNumber)
            );
        }

        public async Task<IActionResult> ApproveSalesOrder(string salesOrderNumber)
        {
            var salesOrder = await salesOrderViewModelService.GetSalesOrder(
                salesOrderNumber
            );

            ViewData["territories"] = 
                (await referenceDataService.GetTerritoriesAsync(
                    salesOrder.SalesOrder.BillToAddress.CountryRegionCode
                ))
                .OrderBy(c => c.Name)
                .ToList()
                .ToSelectList(c => c.Name, c => c.Name);

            ViewData["salesPersons"] =
                new List<SalesPerson>()
                .ToSelectList(sp => sp.Name.FullName, sp => sp.Name.FullName);

            return View(salesOrder);
        }

        [HttpPost]
        public IActionResult ApproveSalesOrder(SalesOrderDetailViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            return RedirectToAction(
                nameof(Detail), 
                vm.SalesOrder.SalesOrderNumber
            );
        }

        public async Task<JsonResult> GetSalesPersons(string territory)
        {
            var salesPersons = await salesPersonViewModelService.GetSalesPersons(territory);
            return Json(salesPersons);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using AW.UI.Web.SharedKernel.SalesOrder.Handlers.GetSalesOrders;
using MediatR;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetTerritories;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.Extensions;
using AW.UI.Web.Admin.Mvc.ViewModels.SalesOrder;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetShipMethods;
using AW.UI.Web.SharedKernel.SalesPerson.Handlers.GetSalesPersons;
using AW.UI.Web.Admin.Mvc.ViewModels.ModelBinders;
using AW.UI.Web.Admin.Mvc.ViewModels.SalesOrder.ModelBinders;

namespace AW.UI.Web.Admin.Mvc.Controllers
{
    public class SalesOrderController : Controller
    {
        private readonly IMediator mediator;
        private readonly ISalesOrderService salesOrderService;
        private readonly ISalesPersonViewModelService salesPersonViewModelService;

        public SalesOrderController(
            IMediator mediator,
            ISalesOrderService salesOrdersViewModelService,
            ISalesPersonViewModelService salesPersonViewModelService
        )
        {
            this.mediator = mediator;
            salesOrderService = salesOrdersViewModelService;
            this.salesPersonViewModelService = salesPersonViewModelService;
        }

        public async Task<IActionResult> Index(int? pageId, string territoryFilterApplied, string customerTypeFilterApplied)
        {
            return View(
                await salesOrderService.GetSalesOrders(
                    pageId ?? 0,
                    Constants.ITEMS_PER_PAGE,
                    territoryFilterApplied,
                    !string.IsNullOrEmpty(customerTypeFilterApplied) ? Enum.Parse<CustomerType>(customerTypeFilterApplied) : default(CustomerType?)
                )
            );
        }

        public async Task<IActionResult> Detail(string salesOrderNumber)
        {
            var salesOrder = await salesOrderService.GetSalesOrderDetail(
                salesOrderNumber
            );

            ViewData["territories"] = (await mediator.Send(new GetTerritoriesQuery()))
                .OrderBy(_ => _.Name)
                .ToList()
                .ToSelectList(_ => _.Name, _ => _.Name);

            ViewData["salesPersons"] = (await mediator.Send(new GetSalesPersonsQuery(salesOrder.SalesOrder.Territory)))
                .OrderBy(_ => _.Name.FullName)
                .ToList()
                .ToSelectList(_ => _.Name.FullName, _ => _.Name.FullName);

            ViewData["shipMethods"] = (await mediator.Send(new GetShipMethodsQuery()))
                .OrderBy(_ => _.Name)
                .ToList()
                .ToSelectList(_ => _.Name, _ => _.Name);

            return View(salesOrder);
        }

        public async Task<IActionResult> ApproveSalesOrder(string salesOrderNumber)
        {
            var salesOrder = await salesOrderService.GetSalesOrderForApproval(
                salesOrderNumber
            );

            ViewData["territories"] =
                (await mediator.Send(new GetTerritoriesQuery(
                        salesOrder.BillToAddress.CountryRegionCode
                    )
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

        public async Task<IActionResult> DuplicateSalesOrder(string salesOrderNumber)
        {
            await salesOrderService.DuplicateSalesOrder(salesOrderNumber);

            return RedirectToAction(
                nameof(Index)
            );
        }

        public async Task<IActionResult> DeleteSalesOrder(string salesOrderNumber)
        {
            await salesOrderService.DeleteSalesOrder(salesOrderNumber);

            return RedirectToAction(
                nameof(Index)
            );
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSalesOrders([FromBody] string[] salesOrderNumbers)
        {
            foreach (var salesOrderNumber in salesOrderNumbers)
            {
                await salesOrderService.DeleteSalesOrder(salesOrderNumber);
            }

            return RedirectToAction(
                nameof(Index)
            );
        }

        public async Task<JsonResult> GetSalesPersons(string territory)
        {
            var result = await salesPersonViewModelService.GetSalesPersons(territory);
            return Json(result.SalesPersons);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderlines([ModelBinder(BinderType = typeof(UpdateOrderlinesViewModelBinder))] UpdateOrderlinesViewModel viewModel)
        {
            await salesOrderService.UpdateOrderlines(viewModel);

            return RedirectToAction(
                nameof(Detail),
                new { salesOrderNumber = viewModel.SalesOrder.SalesOrderNumber }
            );
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderInfo([ModelBinder(BinderType = typeof(UpdateOrderInfoViewModelBinder))] UpdateOrderInfoViewModel viewModel)
        {
            await salesOrderService.UpdateOrderInfo(viewModel);

            return RedirectToAction(
                nameof(Detail),
                new { salesOrderNumber = viewModel.SalesOrder.SalesOrderNumber }
            );
        }
    }
}
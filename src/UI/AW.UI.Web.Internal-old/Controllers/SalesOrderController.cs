using AW.UI.Web.Internal.ApiClients.SalesOrderApi.Models;
using AW.UI.Web.Internal.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.Controllers
{
    public class SalesOrderController : Controller
    {
        private readonly ISalesOrderViewModelService salesOrdersViewModelService;

        public SalesOrderController(ISalesOrderViewModelService salesOrdersViewModelService)
        {
            this.salesOrdersViewModelService = salesOrdersViewModelService;
        }

        public async Task<IActionResult> Index(int? pageId, string territoryFilterApplied, string customerTypeFilterApplied)
        {
            return View(
                await salesOrdersViewModelService.GetSalesOrders(
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
                await salesOrdersViewModelService.GetSalesOrder(
                    salesOrderNumber)
            );
        }
    }
}
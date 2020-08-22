using AutoMapper;
using AW.UI.Web.Internal.Interfaces;
using AW.UI.Web.Internal.SalesOrderService;
using AW.UI.Web.Internal.SalesTerritoryService;
using AW.UI.Web.Internal.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.Services
{
    public class SalesOrdersViewModelService : ISalesOrdersViewModelService
    {
        private readonly ILogger<SalesOrdersViewModelService> logger;
        private readonly IMapper mapper;
        private readonly ISalesOrderService salesOrderService;
        private readonly ISalesTerritoryService salesTerritoryService;

        public SalesOrdersViewModelService(
            ILoggerFactory loggerFactory,
            IMapper mapper,
            ISalesOrderService salesOrderService,
            ISalesTerritoryService salesTerritoryService)
        {
            logger = loggerFactory.CreateLogger<SalesOrdersViewModelService>();
            this.mapper = mapper;
            this.salesOrderService = salesOrderService;
            this.salesTerritoryService = salesTerritoryService;
        }

        public async Task<SalesOrdersIndexViewModel> GetSalesOrders(int pageIndex, int pageSize, string territory, string customerType)
        {
            logger.LogInformation("GetSalesOrders called");

            var response = await salesOrderService.ListSalesOrdersAsync(
                new ListSalesOrdersRequest
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Territory = territory,
                    CustomerType = !string.IsNullOrEmpty(customerType) ? Enum.Parse<CustomerType>(customerType) : default,
                    CustomerTypeSpecified = !string.IsNullOrEmpty(customerType)
                }
            );

            var vm = new SalesOrdersIndexViewModel
            {
                SalesOrders = mapper.Map<List<SalesOrderViewModel>>(response.SalesOrder),
                Territories = await GetTerritories(),
                CustomerTypes = GetCustomerTypes(),
                PaginationInfo = new PaginationInfoViewModel()
                {
                    ActualPage = pageIndex,
                    ItemsPerPage = response.SalesOrder.Length,
                    TotalItems = response.TotalSalesOrders,
                    TotalPages = int.Parse(Math.Ceiling(((decimal)response.TotalSalesOrders / pageSize)).ToString())
                }
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

            return vm;
        }

        private async Task<List<SelectListItem>> GetTerritories()
        {
            logger.LogInformation("GetTerritories called.");
            var territories = await salesTerritoryService.ListTerritoriesAsync(
                new ListTerritoriesRequest()
            );

            var items = territories
                .ListTerritoriesResult
                .Select(t => new SelectListItem() { Value = t.Name, Text = $"{t.Name} ({t.CountryRegionCode})" })
                .OrderBy(b => b.Text)
                .ToList();

            var allItem = new SelectListItem() { Value = "", Text = "All", Selected = true };
            items.Insert(0, allItem);

            return items;
        }

        private List<SelectListItem> GetCustomerTypes()
        {
            logger.LogInformation("GetCustomerTypes called.");

            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = "", Text = "All", Selected = true },
                new SelectListItem() { Value = "Individual", Text = "Individual"},
                new SelectListItem() { Value = "Store", Text = "Store" }
            };

            return items;
        }
    }
}
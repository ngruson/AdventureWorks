using AutoMapper;
using AW.UI.Web.Common.ApiClients.ReferenceDataApi;
using AW.UI.Web.Common.ApiClients.SalesOrderApi;
using AW.UI.Web.Common.ApiClients.SalesOrderApi.Models;
using AW.UI.Web.Internal.Interfaces;
using AW.UI.Web.Internal.ViewModels;
using AW.UI.Web.Internal.ViewModels.SalesOrder;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.Services
{
    public class SalesOrderViewModelService : ISalesOrderViewModelService
    {
        private readonly ILogger<SalesOrderViewModelService> logger;
        private readonly IMapper mapper;
        private readonly IReferenceDataApiClient referenceDataApiClient;
        private readonly ISalesOrderApiClient salesOrderApiClient;

        public SalesOrderViewModelService(
            ILoggerFactory loggerFactory,
            IMapper mapper,            
            IReferenceDataApiClient referenceDataApiClient,
            ISalesOrderApiClient salesOrderApiClient
        )
        {
            logger = loggerFactory.CreateLogger<SalesOrderViewModelService>();
            this.mapper = mapper;            
            this.referenceDataApiClient = referenceDataApiClient;
            this.salesOrderApiClient = salesOrderApiClient;
        }

        public async Task<SalesOrderIndexViewModel> GetSalesOrders(int pageIndex, int pageSize, string territory, CustomerType? customerType)
        {
            logger.LogInformation("GetSalesOrders called");

            var response = await salesOrderApiClient.GetSalesOrdersAsync(
                pageIndex,
                pageSize,
                territory,
                customerType
            );

            var vm = new SalesOrderIndexViewModel
            {
                SalesOrders = mapper.Map<List<SalesOrderViewModel>>(response.SalesOrders),
                Territories = await GetTerritories(),
                CustomerTypes = GetCustomerTypes(),
                PaginationInfo = new PaginationInfoViewModel()
                {
                    ActualPage = pageIndex,
                    ItemsPerPage = response.SalesOrders.Count,
                    TotalItems = response.TotalSalesOrders,
                    TotalPages = int.Parse(Math.Ceiling(((decimal)response.TotalSalesOrders / pageSize)).ToString())
                }
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "disabled" : "";

            return vm;
        }

        private async Task<List<SelectListItem>> GetTerritories()
        {
            logger.LogInformation("GetTerritories called.");
            var territories = await referenceDataApiClient.GetTerritoriesAsync();

            var items = territories
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

        public async Task<SalesOrderDetailViewModel> GetSalesOrder(string salesOrderNumber)
        {
            logger.LogInformation("GetSalesOrder called");
            var response = await salesOrderApiClient.GetSalesOrderAsync(salesOrderNumber);

            var vm = new SalesOrderDetailViewModel
            {
                SalesOrder = mapper.Map<SalesOrderViewModel>(response)
            };

            return vm;
        }
    }
}
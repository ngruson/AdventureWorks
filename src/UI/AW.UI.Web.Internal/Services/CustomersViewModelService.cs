using AutoMapper;
using AW.UI.Web.Internal.CustomerService;
using AW.UI.Web.Internal.Interfaces;
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
    public class CustomersViewModelService : ICustomersViewModelService
    {
        private readonly ILogger<CustomersViewModelService> logger;
        private readonly IMapper mapper;
        private readonly ICustomerService customerService;
        private readonly ISalesTerritoryService salesTerritoryService;

        public CustomersViewModelService(
            ILoggerFactory loggerFactory,
            IMapper mapper,
            ICustomerService customerService,
            ISalesTerritoryService salesTerritoryService)
        {
            logger = loggerFactory.CreateLogger<CustomersViewModelService>();
            this.mapper = mapper;
            this.customerService = customerService;
            this.salesTerritoryService = salesTerritoryService;
        }

        public async Task<CustomersIndexViewModel> GetCustomers(int pageIndex, int pageSize, string territory)
        {
            logger.LogInformation("GetCustomers called");

            var response = await customerService.ListCustomersAsync(
                new ListCustomersRequest
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Territory = territory
                }
            );

            var vm = new CustomersIndexViewModel
            {
                Customers = mapper.Map<List<CustomerViewModel>>(response.Customers),
                Territories = (await GetTerritories()).ToList(),
                PaginationInfo = new PaginationInfoViewModel()
                {
                    ActualPage = pageIndex,
                    ItemsPerPage = response.Customers.Length,
                    TotalItems = response.TotalCustomers,
                    TotalPages = int.Parse(Math.Ceiling(((decimal)response.TotalCustomers / pageSize)).ToString())
                }
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

            return vm;
        }

        private async Task<IEnumerable<SelectListItem>> GetTerritories()
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

            var allItem = new SelectListItem() { Value = null, Text = "All", Selected = true };
            items.Insert(0, allItem);

            return items;
        }
    }
}
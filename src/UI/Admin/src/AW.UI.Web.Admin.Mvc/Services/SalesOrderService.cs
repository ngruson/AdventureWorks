using Ardalis.GuardClauses;
using AutoMapper;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Admin.Mvc.ViewModels.SalesOrder;
using AW.UI.Web.Admin.Mvc.ViewModels;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetTerritories;
using AW.UI.Web.SharedKernel.SalesOrder.Handlers.ApproveSalesOrder;
using AW.UI.Web.SharedKernel.SalesOrder.Handlers.GetSalesOrder;
using AW.UI.Web.SharedKernel.SalesOrder.Handlers.GetSalesOrders;
using AW.UI.Web.SharedKernel.SalesOrder.Handlers.UpdateSalesOrder;
using AW.UI.Web.SharedKernel.SalesPerson.Handlers.GetSalesPersons;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AW.UI.Web.Admin.Mvc.Services
{
    public class SalesOrderService : ISalesOrderService
    {
        private readonly ILogger<SalesOrderService> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public SalesOrderService(
            ILoggerFactory loggerFactory,
            IMapper mapper,
            IMediator mediator
        )
        {
            _logger = loggerFactory.CreateLogger<SalesOrderService>();
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<SalesOrderIndexViewModel> GetSalesOrders(int pageIndex, int pageSize, string territory, SharedKernel.SalesOrder.Handlers.GetSalesOrders.CustomerType? customerType)
        {
            _logger.LogInformation("GetSalesOrders called");

            var response = await _mediator.Send(
                new GetSalesOrdersQuery(
                    pageIndex,
                    pageSize,
                    territory,
                    customerType
                )
            );

            var vm = new SalesOrderIndexViewModel
            {
                SalesOrders = _mapper.Map<List<SalesOrderViewModel>>(response.SalesOrders),
                Territories = await GetTerritories(),
                CustomerTypes = GetCustomerTypes(),
                PaginationInfo = new PaginationInfoViewModel()
                {
                    ActualPage = pageIndex,
                    ItemsPerPage = response.SalesOrders.Count,
                    TotalItems = response.TotalSalesOrders,
                    TotalPages = int.Parse(Math.Ceiling((decimal)response.TotalSalesOrders / pageSize).ToString())
                }
            };

            vm.PaginationInfo.Next = vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1 ? "disabled" : "";
            vm.PaginationInfo.Previous = vm.PaginationInfo.ActualPage == 0 ? "disabled" : "";

            return vm;
        }

        private async Task<List<SelectListItem>> GetTerritories()
        {
            _logger.LogInformation("GetTerritories called.");
            var territories = await _mediator.Send(new GetTerritoriesQuery());

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
            _logger.LogInformation("GetCustomerTypes called.");

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
            _logger.LogInformation("GetSalesOrder called");
            var salesOrder = await _mediator.Send(new GetSalesOrderQuery(salesOrderNumber));

            var vm = new SalesOrderDetailViewModel
            {
                SalesOrder = _mapper.Map<SalesOrderViewModel>(salesOrder)
            };

            return vm;
        }

        public async Task<ApproveSalesOrderViewModel> GetSalesOrderForApproval(string salesOrderNumber)
        {
            _logger.LogInformation("GetSalesOrderForApproval called");
            var salesOrder = await _mediator.Send(new GetSalesOrderQuery(salesOrderNumber));

            return _mapper.Map<ApproveSalesOrderViewModel>(salesOrder);
        }

        public async Task UpdateSalesOrder(SalesOrderViewModel viewModel)
        {
            _logger.LogInformation("Getting sales order for {SalesOrderNumber}", viewModel.SalesOrderNumber);
            var salesOrder = await _mediator.Send(new GetSalesOrderQuery(viewModel.SalesOrderNumber));
            _logger.LogInformation("Retrieved sales order {@SalesOrder}", salesOrder);
            Guard.Against.Null(salesOrder, _logger);

            var salesOrderToUpdate = _mapper.Map<SharedKernel.SalesOrder.Handlers.UpdateSalesOrder.SalesOrder>(salesOrder);
            Guard.Against.Null(salesOrderToUpdate, _logger);

            _mapper.Map(viewModel, salesOrderToUpdate);

            _logger.LogInformation("Updating sales order {@SalesOrder}", salesOrder);
            await _mediator.Send(new UpdateSalesOrderCommand(salesOrderToUpdate));
            _logger.LogInformation("Sales order updated successfully");
        }

        public async Task UpdateSalesOrder(ApproveSalesOrderViewModel viewModel)
        {
            _logger.LogInformation("Getting sales order for {SalesOrderNumber}", viewModel.SalesOrderNumber);
            var salesOrder = await _mediator.Send(new GetSalesOrderQuery(viewModel.SalesOrderNumber));
            _logger.LogInformation("Retrieved sales order {@SalesOrder}", salesOrder);
            Guard.Against.Null(salesOrder, _logger);

            var salesOrderToUpdate = _mapper.Map<SharedKernel.SalesOrder.Handlers.UpdateSalesOrder.SalesOrder>(salesOrder);
            Guard.Against.Null(salesOrderToUpdate, _logger);

            var salesPersons = await _mediator.Send(new GetSalesPersonsQuery(viewModel.Territory));
            var salesPerson = salesPersons.SingleOrDefault(_ => _.Name.FullName == viewModel.SalesPerson);

            salesOrderToUpdate.Territory = viewModel.Territory;
            salesOrderToUpdate.SalesPerson = _mapper.Map<SharedKernel.SalesOrder.Handlers.UpdateSalesOrder.SalesPerson>(salesPerson);

            _logger.LogInformation("Updating sales order {@SalesOrder}", salesOrder);
            await _mediator.Send(new UpdateSalesOrderCommand(salesOrderToUpdate));
            _logger.LogInformation("Sales order updated successfully");
        }

        public async Task ApproveSalesOrder(string salesOrderNumber)
        {
            _logger.LogInformation("Approving sales order {SalesOrderNumber}", salesOrderNumber);
            await _mediator.Send(new ApproveSalesOrderCommand(salesOrderNumber));
            _logger.LogInformation("Sales order approved successfully");
        }
    }
}
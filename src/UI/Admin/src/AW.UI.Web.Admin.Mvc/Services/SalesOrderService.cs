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
using AW.UI.Web.SharedKernel.SalesOrder.Handlers.DuplicateSalesOrder;
using AW.UI.Web.SharedKernel.SalesOrder.Handlers.DeleteSalesOrder;

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

        private async Task<SharedKernel.SalesOrder.Handlers.GetSalesOrder.SalesOrder> GetSalesOrder(string salesOrderNumber)
        {
            _logger.LogInformation("Getting sales order for {SalesOrderNumber}", salesOrderNumber);
            var salesOrder = await _mediator.Send(new GetSalesOrderQuery(salesOrderNumber));
            _logger.LogInformation("Retrieved sales order {@SalesOrder}", salesOrder);
            Guard.Against.Null(salesOrder, _logger);

            return salesOrder;
        }

        private async Task UpdateSalesOrder(SharedKernel.SalesOrder.Handlers.UpdateSalesOrder.SalesOrder salesOrder)
        {
            _logger.LogInformation("Updating sales order {@SalesOrder}", salesOrder);
            await _mediator.Send(new UpdateSalesOrderCommand(salesOrder));
            _logger.LogInformation("Sales order updated successfully");
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

        public async Task<SalesOrderDetailViewModel> GetSalesOrderDetail(string salesOrderNumber)
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
            var salesOrder = await GetSalesOrder(viewModel.SalesOrderNumber);

            var salesOrderToUpdate = _mapper.Map<SharedKernel.SalesOrder.Handlers.UpdateSalesOrder.SalesOrder>(salesOrder);
            Guard.Against.Null(salesOrderToUpdate, _logger);

            _mapper.Map(viewModel, salesOrderToUpdate);

            _logger.LogInformation("Updating sales order {@SalesOrder}", salesOrder);
            await _mediator.Send(new UpdateSalesOrderCommand(salesOrderToUpdate));
            _logger.LogInformation("Sales order updated successfully");
        }

        public async Task UpdateSalesOrder(ApproveSalesOrderViewModel viewModel)
        {
            var salesOrder = await GetSalesOrder(viewModel.SalesOrderNumber);

            var salesOrderToUpdate = _mapper.Map<SharedKernel.SalesOrder.Handlers.UpdateSalesOrder.SalesOrder>(salesOrder);
            Guard.Against.Null(salesOrderToUpdate, _logger);

            var salesPersons = await _mediator.Send(new GetSalesPersonsQuery(viewModel.Territory));
            var salesPerson = salesPersons.SingleOrDefault(_ => _.Name.FullName == viewModel.SalesPerson);

            salesOrderToUpdate.Territory = viewModel.Territory;
            salesOrderToUpdate.SalesPerson = _mapper.Map<SharedKernel.SalesOrder.Handlers.UpdateSalesOrder.SalesPerson>(salesPerson);

            await UpdateSalesOrder(salesOrderToUpdate);
        }

        public async Task ApproveSalesOrder(string salesOrderNumber)
        {
            _logger.LogInformation("Approving sales order {SalesOrderNumber}", salesOrderNumber);
            await _mediator.Send(new ApproveSalesOrderCommand(salesOrderNumber));
            _logger.LogInformation("Sales order approved successfully");
        }

        public async Task DuplicateSalesOrder(string salesOrderNumber)
        {
            _logger.LogInformation("Duplicating sales order {SalesOrderNumber}", salesOrderNumber);
            await _mediator.Send(new DuplicateSalesOrderCommand(salesOrderNumber));
            _logger.LogInformation("Sales order duplicated successfully");
        }

        public async Task DeleteSalesOrder(string salesOrderNumber)
        {
            _logger.LogInformation("Deleting sales order {SalesOrderNumber}", salesOrderNumber);
            await _mediator.Send(new DeleteSalesOrderCommand(salesOrderNumber));
            _logger.LogInformation("Sales order deleted successfully");
        }

        public async Task UpdateOrderlines(UpdateOrderlinesViewModel viewModel)
        {
            var salesOrder = await GetSalesOrder(viewModel.SalesOrder.SalesOrderNumber);

            var salesOrderToUpdate = _mapper.Map<SharedKernel.SalesOrder.Handlers.UpdateSalesOrder.SalesOrder>(salesOrder);
            Guard.Against.Null(salesOrderToUpdate, _logger);

            foreach (var updatedOrderLine in viewModel.SalesOrder.OrderLines)
            {
                var orderLine = salesOrderToUpdate.OrderLines.SingleOrDefault(_ => _.ProductNumber == updatedOrderLine.ProductNumber);
                Guard.Against.Null(orderLine);

                orderLine.OrderQty = short.Parse(updatedOrderLine.OrderQty);
            }

            await UpdateSalesOrder(salesOrderToUpdate);
        }

        public async Task UpdateOrderInfo(UpdateOrderInfoViewModel viewModel)
        {
            var salesOrder = await GetSalesOrder(viewModel.SalesOrder.SalesOrderNumber);

            var salesOrderToUpdate = _mapper.Map<SharedKernel.SalesOrder.Handlers.UpdateSalesOrder.SalesOrder>(salesOrder);
            Guard.Against.Null(salesOrderToUpdate, _logger);

            salesOrderToUpdate.RevisionNumber = byte.Parse(viewModel.SalesOrder.RevisionNumber);
            salesOrderToUpdate.OnlineOrderFlag = viewModel.SalesOrder.OnlineOrderFlag;
            salesOrderToUpdate.DueDate = viewModel.SalesOrder.DueDate;
            salesOrderToUpdate.ShipDate = viewModel.SalesOrder.ShipDate;
            salesOrderToUpdate.PurchaseOrderNumber = viewModel.SalesOrder.PurchaseOrderNumber;
            salesOrderToUpdate.AccountNumber = viewModel.SalesOrder.AccountNumber;
            salesOrderToUpdate.ShipMethod = viewModel.SalesOrder.ShipMethod;
            salesOrderToUpdate.Territory = viewModel.SalesOrder.Territory;

            if (!string.IsNullOrEmpty(viewModel.SalesOrder.SalesPerson))
            {
                var salesPersons = await _mediator.Send(new GetSalesPersonsQuery(salesOrderToUpdate.Territory));
                var salesPerson = salesPersons.SingleOrDefault(_ => _.Name.FullName == viewModel.SalesOrder.SalesPerson);
                Guard.Against.Null(salesPerson, _logger, nameof(salesPerson));

                salesOrderToUpdate.SalesPerson = new SharedKernel.SalesOrder.Handlers.UpdateSalesOrder.SalesPerson
                {
                    Name = salesPerson.Name
                };
            }

            await UpdateSalesOrder(salesOrderToUpdate);
        }
    }
}
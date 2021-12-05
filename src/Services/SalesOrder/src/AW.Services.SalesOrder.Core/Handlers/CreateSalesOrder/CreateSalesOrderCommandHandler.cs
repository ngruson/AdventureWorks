using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.SalesOrder.Core.Entities;
using AW.Services.SalesOrder.Core.IntegrationEvents;
using AW.Services.SalesOrder.Core.IntegrationEvents.Events;
using AW.Services.SalesOrder.Core.Specifications;
using AW.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.SalesOrder.Core.Handlers.CreateSalesOrder
{
    public class CreateSalesOrderCommandHandler : IRequestHandler<CreateSalesOrderCommand, bool>
    {
        private readonly ILogger<CreateSalesOrderCommandHandler> logger;
        private readonly IMapper mapper;
        private readonly ISalesOrderIntegrationEventService salesOrderIntegrationEventService;
        private readonly IRepository<Entities.SalesOrder> salesOrderRepository;
        private readonly IRepository<SpecialOfferProduct> specialOfferProductRepository;

        public CreateSalesOrderCommandHandler(
            ILogger<CreateSalesOrderCommandHandler> logger,
            IMapper mapper,
            ISalesOrderIntegrationEventService salesOrderIntegrationEventService,
            IRepository<Entities.SalesOrder> salesOrderRepository,
            IRepository<SpecialOfferProduct> specialOfferProductRepository
        )
        {
            this.logger = logger;
            this.mapper = mapper;
            this.salesOrderIntegrationEventService = salesOrderIntegrationEventService;
            this.salesOrderRepository = salesOrderRepository;
            this.specialOfferProductRepository = specialOfferProductRepository;
        }

        public async Task<bool> Handle(CreateSalesOrderCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("----- Creating sales order");

            logger.LogInformation("----- Creating OrderStarted domain event");
            // Add Integration event to clean the basket
            var orderStartedIntegrationEvent = new OrderStartedIntegrationEvent(request.UserId);
            await salesOrderIntegrationEventService.AddAndSaveEventAsync(orderStartedIntegrationEvent);

            var billToAddress = mapper.Map<Address>(request.BillToAddress);
            var shipToAddress = mapper.Map<Address>(request.ShipToAddress);
            var salesOrder = new Entities.SalesOrder(request.UserId, request.UserName, request.CustomerNumber, request.ShipMethod, billToAddress, shipToAddress, request.CardType, request.CardNumber, request.CardSecurityNumber, request.CardHolderName, request.CardExpiration);

            foreach (var item in request.OrderItems)
            {
                var specialOfferProduct = await specialOfferProductRepository
                    .GetBySpecAsync(
                        new GetSpecialOfferProductSpecification(item.ProductNumber)
                    );
                Guard.Against.Null(specialOfferProduct, nameof(specialOfferProduct));

                salesOrder.AddOrderLine(item.ProductNumber, item.ProductName, item.UnitPrice, item.Discount, specialOfferProduct, item.Quantity);
            }

            logger.LogInformation("----- Saving sales order to database - Sales Order: {@SalesOrder}", salesOrder);

            await salesOrderRepository.AddAsync(salesOrder);

            logger.LogInformation("Sales order was created succesfully");

            return true;
        }
    }
}
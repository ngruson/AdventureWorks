﻿using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.SalesOrder.Core.Entities;
using AW.Services.SalesOrder.Core.IntegrationEvents;
using AW.Services.SalesOrder.Core.IntegrationEvents.Events;
using AW.Services.SalesOrder.Core.Specifications;
using AW.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly IRepository<Address> addressRepository;

        public CreateSalesOrderCommandHandler(
            ILogger<CreateSalesOrderCommandHandler> logger,
            IMapper mapper,
            ISalesOrderIntegrationEventService salesOrderIntegrationEventService,
            IRepository<Entities.SalesOrder> salesOrderRepository,
            IRepository<SpecialOfferProduct> specialOfferProductRepository,
            IRepository<Address> addressRepository
        )
        {
            this.logger = logger;
            this.mapper = mapper;
            this.salesOrderIntegrationEventService = salesOrderIntegrationEventService;
            this.salesOrderRepository = salesOrderRepository;
            this.specialOfferProductRepository = specialOfferProductRepository;
            this.addressRepository = addressRepository;
        }

        public async Task<bool> Handle(CreateSalesOrderCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("----- Creating sales order");

            logger.LogInformation("----- Creating OrderStarted domain event");
            // Add Integration event to clean the basket
            var orderStartedIntegrationEvent = new OrderStartedIntegrationEvent(request.UserId);
            await salesOrderIntegrationEventService.AddAndSaveEventAsync(orderStartedIntegrationEvent);

            var billToAddress = await GetAddress(request.BillToAddress);
            var shipToAddress = await GetAddress(request.ShipToAddress);
            var salesOrder = new Entities.SalesOrder(request.UserId, request.UserName, request.CustomerNumber, request.ShipMethod, billToAddress, shipToAddress, request.CardType, request.CardNumber, request.CardSecurityNumber, request.CardHolderName, request.CardExpiration);

            foreach (var item in request.OrderItems)
            {
                var specialOfferProduct = await specialOfferProductRepository
                    .GetBySpecAsync(
                        new GetSpecialOfferProductSpecification(item.ProductNumber),
                        cancellationToken
                    );
                Guard.Against.Null(specialOfferProduct, nameof(specialOfferProduct));

                salesOrder.AddOrderLine(item.ProductNumber, item.ProductName, item.UnitPrice, item.Discount, specialOfferProduct, item.Quantity);
            }

            logger.LogInformation("----- Saving sales order to database - Sales Order: {@SalesOrder}", salesOrder);

            await salesOrderRepository.AddAsync(salesOrder, cancellationToken);

            logger.LogInformation("Sales order was created succesfully");

            return true;
        }

        private async Task<Address> GetAddress(AddressDto address)
        {
            Guard.Against.Null(address, nameof(address));

            var existingAddress = await addressRepository.GetBySpecAsync(
                new GetAddressSpecification(
                    address.AddressLine1,
                    address.AddressLine2,
                    address.PostalCode,
                    address.City,
                    address.StateProvinceCode,
                    address.CountryRegionCode
                )
            );

            if (existingAddress == null)
            {
                logger.LogInformation("Address {@Address} not found", address);
                return mapper.Map<Address>(address);
            }

            logger.LogInformation("Address {@Address} found", address);

            return existingAddress;
        }
    }
}
using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Sales.Core.Entities;
using AW.Services.Sales.Core.Guards;
using AW.Services.Sales.Core.IntegrationEvents;
using AW.Services.Sales.Core.IntegrationEvents.Events;
using AW.Services.Sales.Core.Specifications;
using AW.Services.Sales.Core.ValueTypes;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Sales.Core.Handlers.CreateSalesOrder
{
    public class CreateSalesOrderCommandHandler : IRequestHandler<CreateSalesOrderCommand, bool>
    {
        private readonly ILogger<CreateSalesOrderCommandHandler> logger;
        private readonly IMapper mapper;
        private readonly ISalesOrderIntegrationEventService salesOrderIntegrationEventService;
        private readonly IRepository<SalesOrder> salesOrderRepository;
        private readonly IRepository<SpecialOfferProduct> specialOfferProductRepository;
        private readonly IRepository<Customer> customerRepository;
        private readonly IRepository<CreditCard> creditCardRepository;

        public CreateSalesOrderCommandHandler(
            ILogger<CreateSalesOrderCommandHandler> logger,
            IMapper mapper,
            ISalesOrderIntegrationEventService salesOrderIntegrationEventService,
            IRepository<SalesOrder> salesOrderRepository,
            IRepository<SpecialOfferProduct> specialOfferProductRepository,
            IRepository<Customer> customerRepository,
            IRepository<CreditCard> creditCardRepository
        )
        {
            this.logger = logger;
            this.mapper = mapper;
            this.salesOrderIntegrationEventService = salesOrderIntegrationEventService;
            this.salesOrderRepository = salesOrderRepository;
            this.specialOfferProductRepository = specialOfferProductRepository;
            this.customerRepository = customerRepository;
            this.creditCardRepository = creditCardRepository;
        }

        public async Task<bool> Handle(CreateSalesOrderCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("----- Creating sales order");

            logger.LogInformation("----- Creating OrderStarted domain event");
            // Add Integration event to clean the basket
            var orderStartedIntegrationEvent = new OrderStartedIntegrationEvent(request.UserId);
            await salesOrderIntegrationEventService.AddAndSaveEventAsync(orderStartedIntegrationEvent);

            var customer = await GetCustomer(request.CustomerNumber);
            var creditCard = await GetCreditCard(
                request.CardType,
                request.CardNumber,
                request.CardExpiration
            );

            var billToAddress = mapper.Map<Address>(request.BillToAddress);
            var shipToAddress = mapper.Map<Address>(request.ShipToAddress);

            var salesOrder = new SalesOrder(request.UserId, request.UserName, customer, request.ShipMethod, billToAddress, shipToAddress, creditCard, request.CardSecurityNumber, request.CardHolderName);

            foreach (var item in request.OrderItems)
            {
                var specialOfferProduct = await specialOfferProductRepository
                    .SingleOrDefaultAsync(
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

        private async Task<Customer> GetCustomer(string customerNumber)
        {
            var customer = await customerRepository.SingleOrDefaultAsync(
                new GetCustomerSpecification(customerNumber)
            );

            Guard.Against.CustomerNull(customer, customerNumber, logger);

            return customer;
        }

        private async Task<CreditCard> GetCreditCard(
            string cardType,
            string cardNumber,
            DateTime cardExpiration
        )
        {
            var creditCard = await creditCardRepository.SingleOrDefaultAsync(
                new GetCreditCardSpecification(
                    cardNumber
                )
            );

            if (creditCard == null)
            {
                logger.LogInformation("Credit card {CardNumber} not found", cardNumber);
                return new CreditCard
                {
                    CardType = cardType,
                    CardNumber = cardNumber,
                    ExpMonth = byte.Parse(cardExpiration.Month.ToString()),
                    ExpYear = short.Parse(cardExpiration.Year.ToString())
                };
            }

            logger.LogInformation("Credit card {@CreditCard} found", creditCard);

            return creditCard;
        }
    }
}
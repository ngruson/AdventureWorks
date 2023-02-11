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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Sales.Core.Handlers.CreateSalesOrder
{
    public class CreateSalesOrderCommandHandler : IRequestHandler<CreateSalesOrderCommand, bool>
    {
        private readonly ILogger<CreateSalesOrderCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ISalesOrderIntegrationEventService _salesOrderIntegrationEventService;
        private readonly IRepository<SalesOrder> _salesOrderRepository;
        private readonly IRepository<SpecialOfferProduct> _specialOfferProductRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<CreditCard> _creditCardRepository;

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
            _logger = logger;
            _mapper = mapper;
            _salesOrderIntegrationEventService = salesOrderIntegrationEventService;
            _salesOrderRepository = salesOrderRepository;
            _specialOfferProductRepository = specialOfferProductRepository;
            _customerRepository = customerRepository;
            _creditCardRepository = creditCardRepository;
        }

        public async Task<bool> Handle(CreateSalesOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Creating sales order");

            _logger.LogInformation("----- Creating OrderStarted domain event");
            // Add Integration event to clean the basket
            var orderStartedIntegrationEvent = new OrderStartedIntegrationEvent(request.UserId!);
            await _salesOrderIntegrationEventService.AddAndSaveEventAsync(orderStartedIntegrationEvent);

            var customer = await GetCustomer(request.CustomerNumber!);
            var creditCard = await GetCreditCard(
                request.CardType!,
                request.CardNumber!,
                request.CardExpiration
            );

            var billToAddress = _mapper.Map<Address>(request.BillToAddress);
            var shipToAddress = _mapper.Map<Address>(request.ShipToAddress);

            var salesOrder = new SalesOrder(request.UserId!, request.UserName!, request.AccountNumber, customer, request.ShipMethod!, billToAddress, shipToAddress, creditCard, request.CardSecurityNumber!, request.CardHolderName!);

            foreach (var item in request.OrderItems!)
            {
                var specialOffers = await _specialOfferProductRepository
                    .ListAsync(
                        new GetSpecialOfferProductSpecification(item.ProductNumber!),
                        cancellationToken
                    );
                var specialOfferProduct = specialOffers.SingleOrDefault(_ => _?.SpecialOffer?.Type == "No Discount");

                Guard.Against.SpecialOfferProductNull(specialOfferProduct, item.ProductNumber!, _logger);

                salesOrder.AddOrderLine(item.ProductNumber!, item.ProductName!, item.UnitPrice, item.UnitPriceDiscount, specialOfferProduct!, item.OrderQty);
            }

            _logger.LogInformation("----- Saving sales order to database - Sales Order: {@SalesOrder}", salesOrder);

            await _salesOrderRepository.AddAsync(salesOrder, cancellationToken);

            _logger.LogInformation("Sales order was created succesfully");

            return true;
        }        

        private async Task<Customer> GetCustomer(string customerNumber)
        {
            var customer = await _customerRepository.SingleOrDefaultAsync(
                new GetCustomerSpecification(customerNumber)
            );

            Guard.Against.CustomerNull(customer, customerNumber, _logger);

            return customer!;
        }

        private async Task<CreditCard> GetCreditCard(
            string cardType,
            string cardNumber,
            DateTime cardExpiration
        )
        {
            var creditCard = await _creditCardRepository.SingleOrDefaultAsync(
                new GetCreditCardSpecification(
                    cardNumber
                )
            );

            if (creditCard == null)
            {
                _logger.LogInformation("Credit card {CardNumber} not found", cardNumber);
                return new CreditCard
                {
                    CardType = cardType,
                    CardNumber = cardNumber,
                    ExpMonth = byte.Parse(cardExpiration.Month.ToString()),
                    ExpYear = short.Parse(cardExpiration.Year.ToString())
                };
            }

            _logger.LogInformation("Credit card {@CreditCard} found", creditCard);

            return creditCard;
        }
    }
}
using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Sales.Core.Exceptions;
using AW.Services.Sales.Core.Guards;
using AW.Services.Sales.Core.Handlers.CreateSalesOrder;
using AW.Services.Sales.Core.Handlers.GetSalesOrder;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Sales.Core.Handlers.DuplicateSalesOrder
{
    public class DuplicateSalesOrderCommandHandler : IRequestHandler<DuplicateSalesOrderCommand>
    {
        private readonly ILogger<DuplicateSalesOrderCommandHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DuplicateSalesOrderCommandHandler(
            ILogger<DuplicateSalesOrderCommandHandler> logger,
            IMediator mediator,
            IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DuplicateSalesOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting sales order {SalesOrderNumber}", request.SalesOrderNumber);
            var salesOrder = await _mediator.Send(
                new GetSalesOrderQuery(request.SalesOrderNumber),
                cancellationToken            
            );
            Guard.Against.SalesOrderNull(salesOrder, request.SalesOrderNumber, _logger);

            var createSalesOrderCommand = new CreateSalesOrderCommand(
                _mapper.Map<List<SalesOrderItemDto>>(salesOrder.OrderLines),
                null,
                null,
                salesOrder.AccountNumber,
                salesOrder.Customer.CustomerNumber,
                salesOrder.ShipMethod,
                _mapper.Map<CreateSalesOrder.AddressDto>(salesOrder.BillToAddress),
                _mapper.Map<CreateSalesOrder.AddressDto>(salesOrder.ShipToAddress),
                salesOrder.CreditCard.CardNumber,
                null,
                new DateTime(salesOrder.CreditCard.ExpYear, salesOrder.CreditCard.ExpMonth, 1),
                null,
                salesOrder.CreditCard.CardType
            );

            _logger.LogInformation("Creating new sales order");
            var result = await _mediator.Send(createSalesOrderCommand, cancellationToken);
            if (!result)
            {
                var ex = new DuplicateSalesOrderException(request.SalesOrderNumber);
                _logger.LogError(ex, "Duplicating sales order {SalesOrderNumber} failed", request.SalesOrderNumber);
                throw ex;
            }

            return Unit.Value;
        }
    }
}
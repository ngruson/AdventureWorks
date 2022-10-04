using Ardalis.GuardClauses;
using AW.Services.Sales.Core.Guards;
using AW.Services.Sales.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Sales.Core.Handlers.CancelSalesOrder
{
    public class CancelSalesOrderCommandHandler : IRequestHandler<CancelSalesOrderCommand, bool>
    {
        private readonly ILogger<CancelSalesOrderCommandHandler> _logger;
        private readonly IRepository<Entities.SalesOrder> _repository;

        public CancelSalesOrderCommandHandler(
            ILogger<CancelSalesOrderCommandHandler> logger,
            IRepository<Entities.SalesOrder> repository
        )
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> Handle(CancelSalesOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting sales order {SalesOrderNumber}", request.SalesOrderNumber);
            var salesOrder = await _repository.SingleOrDefaultAsync(
                new GetSalesOrderSpecification(request.SalesOrderNumber),
                cancellationToken
            );

            Guard.Against.SalesOrderNull(salesOrder, request.SalesOrderNumber, _logger);

            salesOrder.SetCancelledStatus();
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
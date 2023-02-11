using Ardalis.GuardClauses;
using AW.Services.Sales.Core.Guards;
using AW.Services.Sales.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Sales.Core.Handlers.ShipSalesOrder
{
    public class ShipSalesOrderCommandHandler : IRequestHandler<ShipSalesOrderCommand, bool>
    {
        private readonly ILogger<ShipSalesOrderCommandHandler> _logger;
        private readonly IRepository<Entities.SalesOrder> _repository;

        public ShipSalesOrderCommandHandler(
            ILogger<ShipSalesOrderCommandHandler> logger,
            IRepository<Entities.SalesOrder> repository
        ) => (_logger, _repository) = (logger, repository);

        public async Task<bool> Handle(ShipSalesOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting sales order {SalesOrderNumber}", request.SalesOrderNumber);
            var salesOrder = await _repository.SingleOrDefaultAsync(
                new GetSalesOrderSpecification(request.SalesOrderNumber),
                cancellationToken
            );

            Guard.Against.SalesOrderNull(salesOrder, request.SalesOrderNumber, _logger);

            salesOrder!.SetShippedStatus();
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
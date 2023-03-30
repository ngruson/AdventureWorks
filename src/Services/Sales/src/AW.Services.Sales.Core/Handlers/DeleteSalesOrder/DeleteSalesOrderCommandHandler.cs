using Ardalis.GuardClauses;
using AW.Services.Sales.Core.Guards;
using AW.Services.Sales.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Sales.Core.Handlers.DeleteSalesOrder
{
    public class DeleteSalesOrderCommandHandler : IRequestHandler<DeleteSalesOrderCommand>
    {
        private readonly ILogger<DeleteSalesOrderCommandHandler> _logger;
        private readonly IRepository<Entities.SalesOrder> _repository;

        public DeleteSalesOrderCommandHandler(
            ILogger<DeleteSalesOrderCommandHandler> logger,
            IRepository<Entities.SalesOrder> salesOrderRepository
        ) => (_logger, _repository) = (logger, salesOrderRepository);

        public async Task Handle(DeleteSalesOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting sales order {SalesOrderNumber}", request.SalesOrderNumber);
            var salesOrder = await _repository.SingleOrDefaultAsync(
                new GetSalesOrderSpecification(request.SalesOrderNumber),
                cancellationToken
            );
            Guard.Against.SalesOrderNull(salesOrder, request.SalesOrderNumber!, _logger);

            _logger.LogInformation("Deleting sales order {@SalesOrder}", salesOrder);
            await _repository.DeleteAsync(salesOrder!, cancellationToken);

            _logger.LogInformation("Sales order {SalesOrderNumber} has been deleted", request.SalesOrderNumber);
        }
    }
}

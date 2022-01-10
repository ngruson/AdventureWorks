using Ardalis.GuardClauses;
using AW.Services.SalesOrder.Core.Guards;
using AW.Services.SalesOrder.Core.Specifications;
using AW.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.SalesOrder.Core.Handlers.DeleteSalesOrder
{
    public class DeleteSalesOrderCommandHandler : IRequestHandler<DeleteSalesOrderCommand>
    {
        private readonly ILogger<DeleteSalesOrderCommandHandler> logger;
        private readonly IRepository<Entities.SalesOrder> salesOrderRepository;

        public DeleteSalesOrderCommandHandler(
            ILogger<DeleteSalesOrderCommandHandler> logger,
            IRepository<Entities.SalesOrder> salesOrderRepository
        ) => (this.logger, this.salesOrderRepository) = (logger, salesOrderRepository);

        public async Task<Unit> Handle(DeleteSalesOrderCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting sales order {SalesOrderNumber}", request.SalesOrderNumber);
            var salesOrder = await salesOrderRepository.GetBySpecAsync(
                new GetSalesOrderSpecification(request.SalesOrderNumber),
                cancellationToken
            );
            Guard.Against.SalesOrderNull(salesOrder);

            logger.LogInformation("Deleting sales order {@SalesOrder}", salesOrder);
            await salesOrderRepository.DeleteAsync(salesOrder, cancellationToken);

            logger.LogInformation("Sales order {SalesOrderNumber} has been deleted", request.SalesOrderNumber);
            return Unit.Value;
        }
    }
}
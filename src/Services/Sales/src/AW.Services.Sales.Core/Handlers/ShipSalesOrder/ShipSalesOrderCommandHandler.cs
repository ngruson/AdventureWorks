using AW.Services.Sales.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Sales.Core.Handlers.ShipSalesOrder
{
    public class ShipSalesOrderCommandHandler : IRequestHandler<ShipSalesOrderCommand, bool>
    {
        private readonly ILogger<ShipSalesOrderCommandHandler> logger;
        private readonly IRepository<Entities.SalesOrder> salesOrderRepository;

        public ShipSalesOrderCommandHandler(
            ILogger<ShipSalesOrderCommandHandler> logger,
            IRepository<Entities.SalesOrder> salesOrderRepository
        )
        {
            this.logger = logger;
            this.salesOrderRepository = salesOrderRepository;
        }

        public async Task<bool> Handle(ShipSalesOrderCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting sales order {SalesOrderNumber}", request.SalesOrderNumber);
            var salesOrder = await salesOrderRepository.GetBySpecAsync(
                new GetSalesOrderSpecification(request.SalesOrderNumber),
                cancellationToken
            );

            if (salesOrder == null)
            {
                logger.LogInformation("Sales order {SalesOrderNumber} not found, Result is false", request.SalesOrderNumber);
                return false;
            }

            salesOrder.SetShippedStatus();
            return await salesOrderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
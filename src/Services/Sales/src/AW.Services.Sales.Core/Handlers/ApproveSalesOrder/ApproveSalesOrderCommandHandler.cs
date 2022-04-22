using AW.Services.Sales.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Sales.Core.Handlers.ApproveSalesOrder
{
    public class ApproveSalesOrderCommandHandler : IRequestHandler<ApproveSalesOrderCommand, bool>
    {
        private readonly ILogger<ApproveSalesOrderCommandHandler> logger;
        private readonly IRepository<Entities.SalesOrder> salesOrderRepository;

        public ApproveSalesOrderCommandHandler(
            ILogger<ApproveSalesOrderCommandHandler> logger,
            IRepository<Entities.SalesOrder> salesOrderRepository
        )
        {
            this.logger = logger;
            this.salesOrderRepository = salesOrderRepository;
        }

        public async Task<bool> Handle(ApproveSalesOrderCommand request, CancellationToken cancellationToken)
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

            salesOrder.SetApprovedStatus();
            return await salesOrderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
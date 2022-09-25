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
        private readonly ILogger<CancelSalesOrderCommandHandler> logger;
        private readonly IRepository<Entities.SalesOrder> salesOrderRepository;

        public CancelSalesOrderCommandHandler(
            ILogger<CancelSalesOrderCommandHandler> logger,
            IRepository<Entities.SalesOrder> salesOrderRepository
        )
        {
            this.logger = logger;
            this.salesOrderRepository = salesOrderRepository;
        }

        public async Task<bool> Handle(CancelSalesOrderCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting sales order {SalesOrderNumber}", request.SalesOrderNumber);
            var salesOrder = await salesOrderRepository.SingleOrDefaultAsync(
                new GetSalesOrderSpecification(request.SalesOrderNumber),
                cancellationToken
            );

            if (salesOrder == null)
            {
                logger.LogInformation("Sales order {SalesOrderNumber} not found, Result is false", request.SalesOrderNumber);
                return false;
            }

            salesOrder.SetCancelledStatus();
            return await salesOrderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
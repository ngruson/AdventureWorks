using AW.Services.Sales.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Sales.Core.Handlers.RejectSalesOrder
{
    public class RejectSalesOrderCommandHandler : IRequestHandler<RejectSalesOrderCommand, bool>
    {
        private readonly ILogger<RejectSalesOrderCommandHandler> logger;
        private readonly IRepository<Entities.SalesOrder> salesOrderRepository;

        public RejectSalesOrderCommandHandler(
            ILogger<RejectSalesOrderCommandHandler> logger,
            IRepository<Entities.SalesOrder> salesOrderRepository
        )
        {
            this.logger = logger;
            this.salesOrderRepository = salesOrderRepository;
        }

        public async Task<bool> Handle(RejectSalesOrderCommand request, CancellationToken cancellationToken)
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

            salesOrder.SetRejectedStatus();
            return await salesOrderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
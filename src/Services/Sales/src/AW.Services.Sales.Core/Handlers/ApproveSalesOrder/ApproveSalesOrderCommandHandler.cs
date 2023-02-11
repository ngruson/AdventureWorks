using Ardalis.GuardClauses;
using AW.Services.Sales.Core.Guards;
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
        private readonly ILogger<ApproveSalesOrderCommandHandler> _logger;
        private readonly IRepository<Entities.SalesOrder> _repository;

        public ApproveSalesOrderCommandHandler(
            ILogger<ApproveSalesOrderCommandHandler> logger,
            IRepository<Entities.SalesOrder> repository
        )
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> Handle(ApproveSalesOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting sales order {SalesOrderNumber}", request.SalesOrderNumber);
            var salesOrder = await _repository.SingleOrDefaultAsync(
                new GetSalesOrderSpecification(request.SalesOrderNumber),
                cancellationToken
            );

            Guard.Against.SalesOrderNull(salesOrder, request.SalesOrderNumber, _logger);

            salesOrder!.SetApprovedStatus();
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
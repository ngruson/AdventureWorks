using Ardalis.GuardClauses;
using AW.Services.Sales.Core.Guards;
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
        private readonly ILogger<RejectSalesOrderCommandHandler> _logger;
        private readonly IRepository<Entities.SalesOrder> _repository;

        public RejectSalesOrderCommandHandler(
            ILogger<RejectSalesOrderCommandHandler> logger,
            IRepository<Entities.SalesOrder> repository
        ) => (_logger, _repository) = (logger, repository);

        public async Task<bool> Handle(RejectSalesOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting sales order {SalesOrderNumber}", request.SalesOrderNumber);
            var salesOrder = await _repository.SingleOrDefaultAsync(
                new GetSalesOrderSpecification(request.SalesOrderNumber),
                cancellationToken
            );

            Guard.Against.SalesOrderNull(salesOrder, request.SalesOrderNumber, _logger);

            salesOrder.SetRejectedStatus();
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
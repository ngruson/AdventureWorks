using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.SalesOrder.Handlers.DuplicateSalesOrder
{
    public class DuplicateSalesOrderCommandHandler : IRequestHandler<DuplicateSalesOrderCommand>
    {
        private readonly ILogger<DuplicateSalesOrderCommandHandler> _logger;
        private readonly ISalesOrderApiClient _client;

        public DuplicateSalesOrderCommandHandler(ILogger<DuplicateSalesOrderCommandHandler> logger, ISalesOrderApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<Unit> Handle(DuplicateSalesOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Duplicating sales order {SalesOrderNumber}", request.SalesOrderNumber);
            Guard.Against.NullOrEmpty(request.SalesOrderNumber, _logger);

            await _client.DuplicateSalesOrderAsync(request.SalesOrderNumber!);
            _logger.LogInformation("Duplicated sales order {SalesOrderNumber}", request.SalesOrderNumber);

            return Unit.Value;
        }
    }
}
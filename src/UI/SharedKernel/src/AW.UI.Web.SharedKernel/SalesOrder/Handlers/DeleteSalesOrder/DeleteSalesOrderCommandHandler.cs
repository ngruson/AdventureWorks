using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.SalesOrder.Handlers.DeleteSalesOrder
{
    public class DeleteSalesOrderCommandHandler : IRequestHandler<DeleteSalesOrderCommand>
    {
        private readonly ILogger<DeleteSalesOrderCommandHandler> _logger;
        private readonly ISalesOrderApiClient _client;

        public DeleteSalesOrderCommandHandler(ILogger<DeleteSalesOrderCommandHandler> logger, ISalesOrderApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task Handle(DeleteSalesOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting sales order {SalesOrderNumber}", request.SalesOrderNumber);
            Guard.Against.NullOrEmpty(request.SalesOrderNumber, _logger);

            await _client.DeleteSalesOrderAsync(request.SalesOrderNumber!);
            _logger.LogInformation("Deleted sales order {SalesOrderNumber}", request.SalesOrderNumber);
        }
    }
}

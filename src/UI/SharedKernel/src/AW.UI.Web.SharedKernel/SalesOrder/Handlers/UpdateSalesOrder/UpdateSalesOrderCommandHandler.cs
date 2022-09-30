using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.SalesOrder.Handlers.UpdateSalesOrder
{
    public class UpdateSalesOrderCommandHandler : IRequestHandler<UpdateSalesOrderCommand>
    {
        private readonly ILogger<UpdateSalesOrderCommandHandler> _logger;
        private readonly ISalesOrderApiClient _client;

        public UpdateSalesOrderCommandHandler(ILogger<UpdateSalesOrderCommandHandler> logger, ISalesOrderApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<Unit> Handle(UpdateSalesOrderCommand request, CancellationToken cancellationToken)
        {            
            Guard.Against.Null(request.SalesOrder, _logger);
            
            _logger.LogInformation("Updating sales order {SalesOrderNumber}", request.SalesOrder!.SalesOrderNumber);
            await _client.UpdateSalesOrderAsync(request.SalesOrder!);
            _logger.LogInformation("Updated sales order {SalesOrderNumber}", request.SalesOrder!.SalesOrderNumber);

            return Unit.Value;
        }
    }
}
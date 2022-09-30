using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.SalesOrder.Handlers.GetSalesOrder
{
    public class GetSalesOrderQueryHandler : IRequestHandler<GetSalesOrderQuery, SalesOrder>
    {
        private readonly ILogger<GetSalesOrderQueryHandler> _logger;
        private readonly ISalesOrderApiClient _client;

        public GetSalesOrderQueryHandler(ILogger<GetSalesOrderQueryHandler> logger, ISalesOrderApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<SalesOrder> Handle(GetSalesOrderQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting sales order {SalesOrderNumber} from API", request.SalesOrderNumber);
            
            Guard.Against.NullOrEmpty(request.SalesOrderNumber, _logger);
            var salesOrder = await _client.GetSalesOrderAsync(request.SalesOrderNumber);
            Guard.Against.Null(salesOrder, _logger);

            _logger.LogInformation("Return sales order {SalesOrderNumber}", request.SalesOrderNumber);

            return salesOrder;
        }
    }
}
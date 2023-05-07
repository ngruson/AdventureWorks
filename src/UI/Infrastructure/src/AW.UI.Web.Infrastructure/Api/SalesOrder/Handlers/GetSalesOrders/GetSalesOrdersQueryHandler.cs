using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders
{
    public class GetSalesOrdersQueryHandler : IRequestHandler<GetSalesOrdersQuery, SalesOrdersResult>
    {
        private readonly ILogger<GetSalesOrdersQueryHandler> _logger;
        private readonly ISalesOrderApiClient _client;

        public GetSalesOrdersQueryHandler(ILogger<GetSalesOrdersQueryHandler> logger, ISalesOrderApiClient client) => (_logger, _client) = (logger, client);

        public async Task<SalesOrdersResult> Handle(GetSalesOrdersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting sales orders from API");
            var salesOrdersResult = await _client.GetSalesOrdersAsync(
                request.PageIndex,
                request.PageSize,
                request.Territory,
                request.CustomerType
            );
            Guard.Against.Null(salesOrdersResult, _logger);

            _logger.LogInformation("Returning sales orders");

            return salesOrdersResult!;
        }
    }
}

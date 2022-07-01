using Ardalis.GuardClauses;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.SalesOrder.Handlers.GetSalesOrders
{
    public class GetSalesOrdersQueryHandler : IRequestHandler<GetSalesOrdersQuery, SalesOrdersResult>
    {
        private readonly ILogger<GetSalesOrdersQueryHandler> logger;
        private readonly ISalesOrderApiClient client;

        public GetSalesOrdersQueryHandler(ILogger<GetSalesOrdersQueryHandler> logger, ISalesOrderApiClient client) => (this.logger, this.client) = (logger, client);

        public async Task<SalesOrdersResult> Handle(GetSalesOrdersQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting sales orders from API");
            var salesOrdersResult = await client.GetSalesOrdersAsync(
                request.PageIndex,
                request.PageSize,
                request.Territory,
                request.CustomerType
            );
            Guard.Against.Null(salesOrdersResult, nameof(salesOrdersResult));

            logger.LogInformation("Returning sales orders");

            return salesOrdersResult;
        }
    }
}
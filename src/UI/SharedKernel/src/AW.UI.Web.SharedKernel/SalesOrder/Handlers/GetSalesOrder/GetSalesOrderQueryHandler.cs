using Ardalis.GuardClauses;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.SalesOrder.Handlers.GetSalesOrder
{
    public class GetSalesOrderQueryHandler : IRequestHandler<GetSalesOrderQuery, SalesOrder>
    {
        private readonly ILogger<GetSalesOrderQueryHandler> logger;
        private readonly ISalesOrderApiClient client;

        public GetSalesOrderQueryHandler(ILogger<GetSalesOrderQueryHandler> logger, ISalesOrderApiClient client)
        {
            this.logger = logger;
            this.client = client;
        }

        public async Task<SalesOrder> Handle(GetSalesOrderQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting sales order {SalesOrderNumber} from API", request.SalesOrderNumber);
            
            Guard.Against.NullOrEmpty(request.SalesOrderNumber, nameof(request.SalesOrderNumber));
            var salesOrder = await client.GetSalesOrderAsync(request.SalesOrderNumber);            
            Guard.Against.Null(salesOrder, nameof(salesOrder));

            logger.LogInformation("Return sales order {SalesOrderNumber}", request.SalesOrderNumber);

            return salesOrder;
        }
    }
}
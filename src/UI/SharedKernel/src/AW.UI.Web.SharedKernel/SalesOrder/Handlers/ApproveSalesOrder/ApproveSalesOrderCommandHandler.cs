using Ardalis.GuardClauses;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.SalesOrder.Handlers.ApproveSalesOrder
{
    public class ApproveSalesOrderCommandHandler : IRequestHandler<ApproveSalesOrderCommand>
    {
        private readonly ILogger<ApproveSalesOrderCommandHandler> logger;
        private readonly ISalesOrderApiClient client;

        public ApproveSalesOrderCommandHandler(ILogger<ApproveSalesOrderCommandHandler> logger, ISalesOrderApiClient client)
        {
            this.logger = logger;
            this.client = client;
        }

        public async Task<Unit> Handle(ApproveSalesOrderCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Approving sales order {SalesOrderNumber}", request.SalesOrderNumber);
            Guard.Against.NullOrEmpty(request.SalesOrderNumber, nameof(request.SalesOrderNumber));

            await client.ApproveSalesOrderAsync(request.SalesOrderNumber);
            logger.LogInformation("Approved sales order {SalesOrderNumber}", request.SalesOrderNumber);

            return Unit.Value;
        }
    }
}
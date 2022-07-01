using Ardalis.GuardClauses;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AW.UI.Web.SharedKernel.SalesOrder.Handlers.UpdateSalesOrder
{
    public class UpdateSalesOrderCommandHandler : IRequestHandler<UpdateSalesOrderCommand>
    {
        private readonly ILogger<UpdateSalesOrderCommandHandler> logger;
        private readonly ISalesOrderApiClient client;

        public UpdateSalesOrderCommandHandler(ILogger<UpdateSalesOrderCommandHandler> logger, ISalesOrderApiClient client)
        {
            this.logger = logger;
            this.client = client;
        }

        public async Task<Unit> Handle(UpdateSalesOrderCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating sales order {SalesOrderNumber}", request.SalesOrder?.SalesOrderNumber);
            Guard.Against.Null(request.SalesOrder, nameof(request.SalesOrder));
            await client.UpdateSalesOrderAsync(request.SalesOrder);
            logger.LogInformation("Updated sales order {SalesOrderNumber}", request.SalesOrder?.SalesOrderNumber);

            return Unit.Value;
        }
    }
}
using AW.Services.Infrastructure;
using AW.SharedKernel.EventBus.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Threading;
using System.Threading.Tasks;
using AW.Services.Sales.Core.IntegrationEvents;

namespace AW.Services.Sales.Core.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> logger;
        private readonly IDbContext dbContext;
        private readonly ISalesOrderIntegrationEventService salesOrderIntegrationEventService;

        public TransactionBehavior(IDbContext dbContext,
            ISalesOrderIntegrationEventService salesOrderIntegrationEventService,
            ILogger<TransactionBehavior<TRequest, TResponse>> logger)
        {
            this.dbContext = dbContext ?? throw new ArgumentException(nameof(IDbContext));
            this.salesOrderIntegrationEventService = salesOrderIntegrationEventService ?? throw new ArgumentException(nameof(ISalesOrderIntegrationEventService));
            this.logger = logger ?? throw new ArgumentException(nameof(ILogger));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            var typeName = request.GetGenericTypeName();

            try
            {
                if (dbContext.HasActiveTransaction)
                {
                    return await next();
                }

                await dbContext.Execute(async () =>
                {
                    using var transaction = await dbContext.BeginTransactionAsync();
                    var transactionId = dbContext.CurrentTransactionId;

                    using (LogContext.PushProperty("TransactionContext", transactionId))
                    {
                        logger.LogInformation("----- Begin transaction {TransactionId} for {CommandName} ({@Command})", transactionId, typeName, request);

                        response = await next();

                        logger.LogInformation("----- Commit transaction {TransactionId} for {CommandName}", transactionId, typeName);

                        await dbContext.CommitTransactionAsync(transaction);

                        await salesOrderIntegrationEventService.PublishEventsThroughEventBusAsync(transactionId);
                    }
                });

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "ERROR Handling transaction for {CommandName} ({@Command})", typeName, request);

                throw;
            }
        }
    }
}
using AW.Services.Infrastructure;
using AW.Services.Infrastructure.EventBus.Abstractions;
using AW.Services.Infrastructure.EventBus.Events;
using AW.Services.Infrastructure.EventBus.IntegrationEventLog;
using AW.SharedKernel.Interfaces;
using Microsoft.Extensions.Logging;

namespace AW.Services.Sales.Core.IntegrationEvents
{
    public class SalesOrderIntegrationEventService : ISalesOrderIntegrationEventService
    {
        private readonly IApplication application;
        private readonly IEventBus eventBus;
        private readonly IDbContext dbContext;
        private readonly IIntegrationEventLogService eventLogService;
        private readonly ILogger<SalesOrderIntegrationEventService> logger;

        public SalesOrderIntegrationEventService(
            IApplication application,
            IEventBus eventBus,
            IDbContext dbContext,
            IIntegrationEventLogService eventLogService,
            ILogger<SalesOrderIntegrationEventService> logger
        )
        {
            this.application = application;
            this.eventBus = eventBus;
            this.dbContext = dbContext;
            this.eventLogService = eventLogService;
            this.logger = logger;
        }

        public async Task AddAndSaveEventAsync(IntegrationEvent evt)
        {
            logger.LogInformation("----- Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})", evt.Id, evt);

            await eventLogService.SaveEventAsync(
                evt,
                dbContext.CurrentTransactionId
            );
        }

        public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
        {
            var pendingLogEvents = await eventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId);

            foreach (var logEvt in pendingLogEvents)
            {
                logger.LogInformation("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", logEvt.EventId, application.AppName, logEvt.IntegrationEvent);

                try
                {
                    await eventLogService.MarkEventAsInProgressAsync(logEvt.EventId);
                    eventBus.Publish(logEvt!.IntegrationEvent!);
                    await eventLogService.MarkEventAsPublishedAsync(logEvt.EventId);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "ERROR publishing integration event: {IntegrationEventId} from {AppName}", logEvt.EventId, application.AppName);

                    await eventLogService.MarkEventAsFailedAsync(logEvt.EventId);
                }
            }
        }
    }
}
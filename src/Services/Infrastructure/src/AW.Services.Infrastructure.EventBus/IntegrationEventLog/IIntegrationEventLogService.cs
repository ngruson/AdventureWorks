using AW.Services.Infrastructure.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.Services.Infrastructure.EventBus.IntegrationEventLog
{
    public interface IIntegrationEventLogService
    {
        Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId);
        Task SaveEventAsync(IntegrationEvent @event, Guid transactionId);
        Task MarkEventAsPublishedAsync(Guid eventId);
        Task MarkEventAsInProgressAsync(Guid eventId);
        Task MarkEventAsFailedAsync(Guid eventId);
    }
}
using AW.Services.SharedKernel.EFCore;
using AW.SharedKernel.EventBus.Events;
using AW.SharedKernel.EventBus.IntegrationEventLog;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AW.SharedKernel.EventBus.EFCore
{
    public class IntegrationEventLogService : IIntegrationEventLogService
    {
        private readonly AWContext dbContext;
        private readonly List<Type> eventTypes;

        public IntegrationEventLogService(
            AWContext dbContext,
            Assembly eventTypesAssembly
        )
        {
            this.dbContext = dbContext;

            eventTypes = eventTypesAssembly
                .GetTypes()
                .Where(t => t.Name.EndsWith(nameof(IntegrationEvent)))
                .ToList();
        }

        public async Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId)
        {
            var tid = transactionId.ToString();

            var result = await dbContext.Set<IntegrationEventLogEntry>()
                .Where(e => e.TransactionId == tid && e.State == EventStateEnum.NotPublished)
                .ToListAsync();

            if (result != null && result.Any())
            {
                return result.OrderBy(o => o.CreationTime)
                    .Select(e => e.DeserializeJsonContent(eventTypes.Find(t => t.Name == e.EventTypeShortName)));
            }

            return new List<IntegrationEventLogEntry>();
        }

        public async Task SaveEventAsync(IntegrationEvent @event, Guid transactionId)
        {
            var eventLogEntry = new IntegrationEventLogEntry(@event, transactionId);

            await dbContext.Set<IntegrationEventLogEntry>().AddAsync(eventLogEntry);

            await dbContext.SaveChangesAsync();
        }

        public Task MarkEventAsPublishedAsync(Guid eventId)
        {
            return UpdateEventStatus(eventId, EventStateEnum.Published);
        }

        public Task MarkEventAsInProgressAsync(Guid eventId)
        {
            return UpdateEventStatus(eventId, EventStateEnum.InProgress);
        }

        public Task MarkEventAsFailedAsync(Guid eventId)
        {
            return UpdateEventStatus(eventId, EventStateEnum.PublishedFailed);
        }

        private async Task UpdateEventStatus(Guid eventId, EventStateEnum status)
        {
            var eventLogEntry = dbContext.Set<IntegrationEventLogEntry>()
                .Single(ie => ie.EventId == eventId);
            eventLogEntry.State = status;

            if (status == EventStateEnum.InProgress)
                eventLogEntry.TimesSent++;

            dbContext.Set<IntegrationEventLogEntry>().Update(eventLogEntry);
            await dbContext.SaveChangesAsync();
        }
    }
}
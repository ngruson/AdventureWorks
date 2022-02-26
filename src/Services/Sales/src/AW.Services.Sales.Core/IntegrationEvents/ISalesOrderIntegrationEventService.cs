using AW.Services.Infrastructure.EventBus.Events;
using System;
using System.Threading.Tasks;

namespace AW.Services.Sales.Core.IntegrationEvents
{
    public interface ISalesOrderIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
}
using AW.SharedKernel.EventBus.Events;
using System;
using System.Threading.Tasks;

namespace AW.Services.SalesOrder.Core.IntegrationEvents
{
    public interface ISalesOrderIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
}
using AW.SharedKernel.EventBus.Events;

namespace AW.Services.SalesOrder.Core.IntegrationEvents.Events
{
    public class OrderStartedIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; private set; }

        public OrderStartedIntegrationEvent(string userId)
            => UserId = userId;
    }
}
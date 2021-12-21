using AW.SharedKernel.EventBus.Events;

namespace AW.Services.SalesOrder.Core.IntegrationEvents.Events
{
    public record OrderStartedIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; private init; }

        public OrderStartedIntegrationEvent(string userId)
            => UserId = userId;
    }
}
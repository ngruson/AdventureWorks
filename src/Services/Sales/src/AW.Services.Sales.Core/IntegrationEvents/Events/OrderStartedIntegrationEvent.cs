using AW.Services.Infrastructure.EventBus.Events;

namespace AW.Services.Sales.Core.IntegrationEvents.Events
{
    public record OrderStartedIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; private init; }

        public OrderStartedIntegrationEvent(string userId)
            => UserId = userId;
    }
}
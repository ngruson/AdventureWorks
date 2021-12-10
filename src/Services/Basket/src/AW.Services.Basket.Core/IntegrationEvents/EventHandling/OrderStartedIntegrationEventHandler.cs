using AW.Services.Basket.Core.IntegrationEvents.Events;
using AW.SharedKernel.EventBus.Abstractions;
using AW.SharedKernel.Interfaces;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Threading.Tasks;

namespace AW.Services.Basket.Core.IntegrationEvents.EventHandling
{
    public class OrderStartedIntegrationEventHandler : IIntegrationEventHandler<OrderStartedIntegrationEvent>
    {
        private readonly IBasketRepository repository;
        private readonly IApplication application;
        private readonly ILogger<OrderStartedIntegrationEventHandler> logger;

        public OrderStartedIntegrationEventHandler(
            IBasketRepository repository,
            IApplication application,
            ILogger<OrderStartedIntegrationEventHandler> logger)
        {
            this.repository = repository;
            this.application = application;
            this.logger = logger;
        }

        public async Task Handle(OrderStartedIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{application.AppName}"))
            {
                logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, application.AppName, @event);

                await repository.DeleteBasketAsync(@event.UserId.ToString());

                logger.LogInformation("----- Basket deleted for user {User}", @event.UserId);
            }
        }
    }
}
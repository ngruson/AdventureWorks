using AW.SharedKernel.EventBus.Abstractions;
using AW.SharedKernel.EventBus.Events;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AW.SharedKernel.EventBus.AzureServiceBus
{
    public class EventBusServiceBus : IEventBus
    {
        private readonly IServiceBusPersisterConnection serviceBusPersisterConnection;
        private readonly ILogger<EventBusServiceBus> logger;
        private readonly IEventBusSubscriptionsManager subsManager;
        private readonly IServiceProvider serviceProvider;
        private readonly string topicName;
        private const string INTEGRATION_EVENT_SUFFIX = "IntegrationEvent";

        public EventBusServiceBus(
            IServiceProvider serviceProvider,
            IServiceBusPersisterConnection serviceBusPersisterConnection,
            ILogger<EventBusServiceBus> logger,
            IEventBusSubscriptionsManager subsManager,
            string topicName
        )
        {
            this.serviceProvider = serviceProvider;
            this.serviceBusPersisterConnection = serviceBusPersisterConnection;
            this.logger = logger;
            this.subsManager = subsManager;
            this.topicName = topicName;

            RegisterSubscriptionClientMessageHandler();
            StartReceiving();
        }

        private void StartReceiving()
        {
            serviceBusPersisterConnection.StartReceiving();
        }

        private void StopReceiving()
        {
            serviceBusPersisterConnection.StopReceiving();
        }

        public void Publish(IntegrationEvent @event)
        {
            var eventName = @event.GetType().Name.Replace(INTEGRATION_EVENT_SUFFIX, "");

            var message = new ServiceBusMessage
            {
                MessageId = Guid.NewGuid().ToString(),
                Body = new BinaryData(@event),
                Subject = eventName
            };

            var sender = serviceBusPersisterConnection.ServiceBusClient.CreateSender(topicName);

            sender.SendMessageAsync(message)
                .GetAwaiter().GetResult();
        }

        public void SubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            logger.LogInformation("Subscribing to dynamic event {EventName} with {EventHandler}", eventName, typeof(TH).Name);

            subsManager.AddDynamicSubscription<TH>(eventName);
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = typeof(T).Name.Replace(INTEGRATION_EVENT_SUFFIX, "");

            var containsKey = subsManager.HasSubscriptionsForEvent<T>();
            if (!containsKey)
            {
                try
                {
                    serviceBusPersisterConnection.AdminClient.CreateSubscriptionAsync(
                        new CreateSubscriptionOptions(topicName, eventName),
                        new CreateRuleOptions
                        {
                            Name = eventName,
                            Filter = new CorrelationRuleFilter
                            {
                                Subject = eventName
                            }
                        }
                    )
                    .GetAwaiter().GetResult();
                }
                catch (Exception) //ServiceBusException
                {
                    logger.LogWarning("The messaging entity {eventName} already exists.", eventName);
                }
            }

            logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH).Name);

            subsManager.AddSubscription<T, TH>();
            StartReceiving();
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = typeof(T).Name.Replace(INTEGRATION_EVENT_SUFFIX, "");

            try
            {
                serviceBusPersisterConnection
                    .AdminClient
                    .DeleteSubscriptionAsync(topicName, eventName)
                    .GetAwaiter().GetResult();
            }
            catch (Exception) //MessagingEntityNotFoundException
            {
                logger.LogWarning("The messaging entity {eventName} Could not be found.", eventName);
            }

            logger.LogInformation("Unsubscribing from event {EventName}", eventName);

            StopReceiving();

            subsManager.RemoveSubscription<T, TH>();
        }

        public void UnsubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            logger.LogInformation("Unsubscribing from dynamic event {EventName}", eventName);

            subsManager.RemoveDynamicSubscription<TH>(eventName);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            subsManager.Clear();
        }

        private void RegisterSubscriptionClientMessageHandler()
        {
            serviceBusPersisterConnection
                .Processor
                .ProcessMessageAsync += MessageHandler;

            serviceBusPersisterConnection
                .Processor
                .ProcessErrorAsync += ExceptionReceivedHandler;
        }

        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            var eventName = $"{args.Message.Subject}{INTEGRATION_EVENT_SUFFIX}";
            var messageData = Encoding.UTF8.GetString(args.Message.Body.ToArray());

            // Complete the message so that it is not received again.
            if (await ProcessEvent(eventName, messageData))
            {
                await args.CompleteMessageAsync(args.Message);
            }
        }

        private Task ExceptionReceivedHandler(ProcessErrorEventArgs args)
        {
            logger.LogError(args.Exception, "ERROR handling message: {ExceptionMessage}", args.Exception.Message);
            return Task.CompletedTask;
        }

        private async Task<bool> ProcessEvent(string eventName, string message)
        {
            var processed = false;
            if (subsManager.HasSubscriptionsForEvent(eventName))
            {
                var subscriptions = subsManager.GetHandlersForEvent(eventName);
                foreach (var subscription in subscriptions)
                {
                    if (subscription.IsDynamic)
                    {
                        if (serviceProvider.GetService(subscription.HandlerType) is not IDynamicIntegrationEventHandler handler) continue;

                        using dynamic eventData = JsonDocument.Parse(message);
                        await handler.Handle(eventData);
                    }
                    else
                    {
                        var handler = serviceProvider.GetService(subscription.HandlerType);
                        if (handler == null) continue;
                        var eventType = subsManager.GetEventTypeByName(eventName);
                        var integrationEvent = JsonSerializer.Deserialize(message, eventType);
                        var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                        await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                    }
                }
                processed = true;
            }
            return processed;
        }
    }
}
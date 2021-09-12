using Microsoft.Azure.ServiceBus;
using System;

namespace AW.SharedKernel.Api.EventBusServiceBus
{
    public interface IServiceBusPersisterConnection : IDisposable
    {
        ITopicClient TopicClient { get; }
        ISubscriptionClient SubscriptionClient { get; }
    }
}
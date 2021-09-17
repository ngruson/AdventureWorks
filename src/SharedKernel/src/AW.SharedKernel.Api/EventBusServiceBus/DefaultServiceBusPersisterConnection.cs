using Microsoft.Azure.ServiceBus;
using System;

namespace AW.SharedKernel.Api.EventBusServiceBus
{
    public class DefaultServiceBusPersisterConnection : IServiceBusPersisterConnection, IDisposable
    {
        private readonly ServiceBusConnectionStringBuilder serviceBusConnectionStringBuilder;
        private readonly string subscriptionClientName;
        private SubscriptionClient subscriptionClient;
        private ITopicClient topicClient;

        bool disposed;

        public DefaultServiceBusPersisterConnection(ServiceBusConnectionStringBuilder serviceBusConnectionStringBuilder,
            string subscriptionClientName)
        {
            this.serviceBusConnectionStringBuilder = serviceBusConnectionStringBuilder ??
                throw new ArgumentNullException(nameof(serviceBusConnectionStringBuilder));
            this.subscriptionClientName = subscriptionClientName;
            subscriptionClient = new SubscriptionClient(serviceBusConnectionStringBuilder, subscriptionClientName);
            topicClient = new TopicClient(serviceBusConnectionStringBuilder, RetryPolicy.Default);
        }

        public ITopicClient TopicClient
        {
            get
            {
                if (topicClient.IsClosedOrClosing)
                {
                    topicClient = new TopicClient(serviceBusConnectionStringBuilder, RetryPolicy.Default);
                }
                return topicClient;
            }
        }

        public ISubscriptionClient SubscriptionClient
        {
            get
            {
                if (subscriptionClient.IsClosedOrClosing)
                {
                    subscriptionClient = new SubscriptionClient(serviceBusConnectionStringBuilder, subscriptionClientName);
                }
                return subscriptionClient;
            }
        }

        public ServiceBusConnectionStringBuilder ServiceBusConnectionStringBuilder => serviceBusConnectionStringBuilder;

        public ITopicClient CreateModel()
        {
            return TopicClient;
        }

        public void Dispose()
        {
            if (disposed) return;

            disposed = true;

            GC.SuppressFinalize(this);
        }
    }
}
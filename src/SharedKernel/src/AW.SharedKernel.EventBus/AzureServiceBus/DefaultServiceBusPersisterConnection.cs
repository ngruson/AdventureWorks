using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using System;
using System.Threading.Tasks;

namespace AW.SharedKernel.EventBus.AzureServiceBus
{
    public class DefaultServiceBusPersisterConnection : IServiceBusPersisterConnection, IDisposable
    {
        private readonly string connectionString;
        private ServiceBusAdministrationClient subscriptionClient;
        private ServiceBusClient serviceBusClient;
        private ServiceBusProcessor processor;

        bool disposed;

        public DefaultServiceBusPersisterConnection(string connectionString, string topicName)
        {
            this.connectionString = connectionString;            
            subscriptionClient = new ServiceBusAdministrationClient(connectionString);
            serviceBusClient = new ServiceBusClient(connectionString);
            
            processor = serviceBusClient.CreateProcessor(
                topicName,
                new ServiceBusProcessorOptions
                {
                    AutoCompleteMessages = false,
                    MaxConcurrentCalls = 10
                }
                );
        }

        public ServiceBusClient ServiceBusClient
        {
            get
            {
                if (serviceBusClient.IsClosed)
                {
                    serviceBusClient = new ServiceBusClient(connectionString);
                }
                return serviceBusClient;
            }
        }

        public ServiceBusAdministrationClient AdminClient
        {
            get
            {
                return subscriptionClient;
            }
        }

        public ServiceBusProcessor Processor
        {
            get { return processor; }
        }

        public async Task StartReceiving()
        {
            await processor.StartProcessingAsync();
        }
        public async Task StopReceiving()
        {
            await processor.StopProcessingAsync();
        }

        public ServiceBusClient CreateModel()
        {
            return ServiceBusClient;
        }

        public void Dispose()
        {
            if (disposed) return;

            disposed = true;

            GC.SuppressFinalize(this);
        }
    }
}
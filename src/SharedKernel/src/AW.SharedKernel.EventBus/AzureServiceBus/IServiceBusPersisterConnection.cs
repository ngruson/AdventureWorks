using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using System;
using System.Threading.Tasks;

namespace AW.SharedKernel.EventBus.AzureServiceBus
{
    public interface IServiceBusPersisterConnection : IDisposable
    {
        ServiceBusClient ServiceBusClient { get; }
        ServiceBusProcessor Processor { get; }
        ServiceBusAdministrationClient AdminClient { get; }

        Task StartReceiving();
        Task StopReceiving();
    }
}
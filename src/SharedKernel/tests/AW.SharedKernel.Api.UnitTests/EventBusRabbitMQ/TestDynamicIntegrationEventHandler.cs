﻿using AW.SharedKernel.Api.EventBus.Abstractions;
using System.Threading.Tasks;

namespace AW.SharedKernel.Api.UnitTests.EventBusRabbitMQ
{
    public class TestDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler
    {
        public Task Handle(dynamic eventData)
        {
            return Task.CompletedTask;
        }
    }
}
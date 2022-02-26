using AW.Services.Infrastructure.EventBus.Abstractions;
using System.Threading.Tasks;

namespace AW.Services.Infrastructure.EventBus.UnitTests
{
    public class TestIntegrationEventHandler : IIntegrationEventHandler<TestIntegrationEvent>
    {
        public Task Handle(TestIntegrationEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}
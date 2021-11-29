using AW.SharedKernel.EventBus.Abstractions;
using System.Threading.Tasks;

namespace AW.SharedKernel.EventBus.UnitTests
{
    public class TestIntegrationEventHandler : IIntegrationEventHandler<TestIntegrationEvent>
    {
        public Task Handle(TestIntegrationEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}
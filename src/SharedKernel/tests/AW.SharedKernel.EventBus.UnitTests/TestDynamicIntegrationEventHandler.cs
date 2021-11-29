using AW.SharedKernel.EventBus.Abstractions;
using System.Threading.Tasks;

namespace AW.SharedKernel.EventBus.UnitTests
{
    public class TestDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler
    {
        public Task Handle(dynamic eventData)
        {
            return Task.CompletedTask;
        }
    }
}
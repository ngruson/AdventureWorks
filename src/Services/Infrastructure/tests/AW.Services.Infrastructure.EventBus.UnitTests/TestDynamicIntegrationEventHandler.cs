using AW.Services.Infrastructure.EventBus.Abstractions;
using System.Threading.Tasks;

namespace AW.Services.Infrastructure.EventBus.UnitTests
{
    public class TestDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler
    {
        public Task Handle(dynamic eventData)
        {
            return Task.CompletedTask;
        }
    }
}
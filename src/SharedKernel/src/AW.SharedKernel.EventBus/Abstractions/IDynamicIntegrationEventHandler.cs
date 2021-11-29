using System.Threading.Tasks;

namespace AW.SharedKernel.EventBus.Abstractions
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
using System.Threading.Tasks;

namespace AW.SharedKernel.Api.EventBus.Abstractions
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
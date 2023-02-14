using MediatR;

namespace AW.Services.SharedKernel.Domain
{
    public abstract class Entity
    {
        private readonly List<INotification> domainEvents = new();
        public IReadOnlyCollection<INotification> DomainEvents => domainEvents.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            domainEvents.Clear();
        }
    }
}

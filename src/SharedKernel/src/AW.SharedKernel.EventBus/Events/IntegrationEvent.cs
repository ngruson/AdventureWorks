using System;
using System.Text.Json.Serialization;

namespace AW.SharedKernel.EventBus.Events
{
    #if NET
    public record IntegrationEvent
    #elif NETSTANDARD
    public abstract class IntegrationEvent
    #endif
    {
        protected IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        [JsonConstructor]
        protected IntegrationEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        [JsonInclude]
#if NET
        public Guid Id { get; private init; }
#elif NETSTANDARD
        public Guid Id { get; private set; }
#endif

        [JsonInclude]
#if NET
        public DateTime CreationDate { get; private set; }
#elif NETSTANDARD
        public DateTime CreationDate { get; private set; }
#endif
    }
}
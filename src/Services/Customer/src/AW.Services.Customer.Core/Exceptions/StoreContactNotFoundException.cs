using AW.Services.Infrastructure;
using System.Runtime.Serialization;

namespace AW.Services.Customer.Core.Exceptions
{
    [Serializable]
    public class StoreContactNotFoundException : DomainException
    {
        public StoreContactNotFoundException(Guid objectId)
            : base($"Contact {objectId} not found")
        { }

        protected StoreContactNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}

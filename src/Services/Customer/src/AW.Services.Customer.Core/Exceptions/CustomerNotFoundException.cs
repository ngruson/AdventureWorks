using AW.Services.Infrastructure;
using System.Runtime.Serialization;

namespace AW.Services.Customer.Core.Exceptions
{
    [Serializable]
    public class CustomerNotFoundException : DomainException
    {
        public CustomerNotFoundException(Guid objectId)
            : base($"Customer {objectId} not found")
        { }

        protected CustomerNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}

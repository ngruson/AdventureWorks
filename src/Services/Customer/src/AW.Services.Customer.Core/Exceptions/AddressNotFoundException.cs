using AW.Services.Infrastructure;
using System.Runtime.Serialization;

namespace AW.Services.Customer.Core.Exceptions
{
    [Serializable]
    public class AddressNotFoundException : DomainException
    {
        public AddressNotFoundException(Guid objectId)
            : base($"Address {objectId} not found")
        { }

        protected AddressNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}

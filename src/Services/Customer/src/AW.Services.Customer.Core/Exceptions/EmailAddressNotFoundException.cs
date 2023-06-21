using AW.Services.Infrastructure;
using System.Runtime.Serialization;

namespace AW.Services.Customer.Core.Exceptions
{
    [Serializable]
    public class EmailAddressNotFoundException : DomainException
    {
        public EmailAddressNotFoundException(Guid objectId)
            : base($"Email address {objectId} not found")
        { }

        protected EmailAddressNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}

using AW.Services.Infrastructure;
using System.Runtime.Serialization;

namespace AW.Services.Customer.Core.Exceptions
{
    [Serializable]
    public class PhoneNumberNotFoundException : DomainException
    {
        public PhoneNumberNotFoundException(Guid objectId)
            : base($"Phone number {objectId} not found")
        { }

        protected PhoneNumberNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}

using AW.Services.Infrastructure;
using System.Runtime.Serialization;

namespace AW.Services.Customer.Core.Exceptions
{
    [Serializable]
    public class StoreContactNotFoundException : DomainException
    {
        public StoreContactNotFoundException(string accountNumber, string? contactName, string contactType)
            : base($"Contact (name: {contactName}, type: {contactType}) for customer {accountNumber} not found")
        { }

        protected StoreContactNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}
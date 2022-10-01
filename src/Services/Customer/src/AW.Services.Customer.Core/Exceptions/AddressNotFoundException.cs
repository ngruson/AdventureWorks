using AW.Services.Infrastructure;
using System;
using System.Runtime.Serialization;

namespace AW.Services.Customer.Core.Exceptions
{
    [Serializable]
    public class AddressNotFoundException : DomainException
    {
        public AddressNotFoundException(string accountNumber, string addressType)
            : base($"{addressType} address for customer {accountNumber} not found")
        { }

        protected AddressNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}
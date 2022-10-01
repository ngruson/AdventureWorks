using AW.Services.Infrastructure;
using System;
using System.Runtime.Serialization;

namespace AW.Services.Customer.Core.Exceptions
{
    [Serializable]
    public class EmailAddressNotFoundException : DomainException
    {
        public EmailAddressNotFoundException(string accountNumber, string emailAddress)
            : base($"Email address {emailAddress} for customer {accountNumber} not found")
        { }

        protected EmailAddressNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}
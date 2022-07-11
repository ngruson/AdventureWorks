using AW.Services.Infrastructure;
using System;
using System.Runtime.Serialization;

namespace AW.Services.Customer.Core.Exceptions
{
    [Serializable]
    public class CustomerNotFoundException : DomainException
    {
        public CustomerNotFoundException(string accountNumber)
            : base($"Customer {accountNumber} not found")
        { }

        protected CustomerNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}
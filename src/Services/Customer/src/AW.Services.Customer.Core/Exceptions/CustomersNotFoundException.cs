using AW.Services.Infrastructure;
using System;
using System.Runtime.Serialization;

namespace AW.Services.Customer.Core.Exceptions
{
    [Serializable]
    public class CustomersNotFoundException : DomainException
    {
        public CustomersNotFoundException()
            : base($"Customers not found")
        { }

        protected CustomersNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}
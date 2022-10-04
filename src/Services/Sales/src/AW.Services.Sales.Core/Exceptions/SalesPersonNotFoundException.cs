using AW.Services.Infrastructure;
using System;
using System.Runtime.Serialization;

namespace AW.Services.Sales.Core.Exceptions
{
    [Serializable]
    public class SalesPersonNotFoundException : DomainException
    {
        public SalesPersonNotFoundException(string name)
            : base($"Sales person {name} not found")
        { }

        protected SalesPersonNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}
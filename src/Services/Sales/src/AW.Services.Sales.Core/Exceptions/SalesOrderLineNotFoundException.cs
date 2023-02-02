using AW.Services.Infrastructure;
using System;
using System.Runtime.Serialization;

namespace AW.Services.Sales.Core.Exceptions
{
    [Serializable]
    public class SalesOrderLineNotFoundException : DomainException
    {
        public SalesOrderLineNotFoundException(string productNumber)
            : base($"Sales order line for product number {productNumber} not found")
        { }

        protected SalesOrderLineNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}
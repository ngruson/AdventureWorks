using AW.Services.Infrastructure;
using System;
using System.Runtime.Serialization;

namespace AW.Services.Product.Core.Exceptions
{
    [Serializable]
    public class ProductNotFoundException : DomainException
    {
        public ProductNotFoundException(string productNumber)
            : base($"Product {productNumber} not found")
        { }

        protected ProductNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}
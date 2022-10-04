using AW.Services.Infrastructure;
using System;
using System.Runtime.Serialization;

namespace AW.Services.Product.Core.Exceptions
{
    [Serializable]
    public class ProductCategoriesNotFoundException : DomainException
    {
        public ProductCategoriesNotFoundException()
            : base($"Product categories not found")
        { }

        protected ProductCategoriesNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}
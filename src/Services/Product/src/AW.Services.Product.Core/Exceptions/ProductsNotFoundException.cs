using AW.Services.Infrastructure;
using System;
using System.Runtime.Serialization;

namespace AW.Services.Product.Core.Exceptions
{
    [Serializable]
    public class ProductsNotFoundException : DomainException
    {
        public ProductsNotFoundException()
            : base($"Products not found")
        { }

        protected ProductsNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}
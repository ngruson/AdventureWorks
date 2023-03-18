using AW.Services.Infrastructure;
using System.Runtime.Serialization;

namespace AW.Services.Product.Core.Exceptions
{
    [Serializable]
    public class ProductModelsNotFoundException : DomainException
    {
        public ProductModelsNotFoundException()
            : base($"Product models not found")
        { }

        protected ProductModelsNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}

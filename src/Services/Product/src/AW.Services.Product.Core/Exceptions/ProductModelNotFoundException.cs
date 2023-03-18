using System.Runtime.Serialization;
using AW.Services.Infrastructure;

namespace AW.Services.Product.Core.Exceptions
{
    [Serializable]
    public class ProductModelNotFoundException : DomainException
    {
        public ProductModelNotFoundException(string name)
            : base($"Product model {name} not found")
        { }

        protected ProductModelNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}

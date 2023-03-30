using AW.Services.Infrastructure;
using System.Runtime.Serialization;

namespace AW.Services.Product.Core.Exceptions
{
    [Serializable]
    public class DuplicateProductException : DomainException
    {
        public DuplicateProductException(string? productNumber, Exception exception)
            : base($"Duplicating product {productNumber} failed", exception)
        { }

        protected DuplicateProductException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}

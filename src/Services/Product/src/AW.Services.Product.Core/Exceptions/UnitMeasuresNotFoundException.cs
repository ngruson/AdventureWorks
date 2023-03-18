using System.Runtime.Serialization;
using AW.Services.Infrastructure;

namespace AW.Services.Product.Core.Exceptions
{
    [Serializable]
    public class UnitMeasuresNotFoundException : DomainException
    {
        public UnitMeasuresNotFoundException()
            : base($"Unit measures not found")
        { }

        protected UnitMeasuresNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}

using System.Runtime.Serialization;
using AW.Services.Infrastructure;

namespace AW.Services.Product.Core.Exceptions
{
    [Serializable]
    public class UnitMeasureNotFoundException : DomainException
    {
        public UnitMeasureNotFoundException(string code)
            : base($"Unit measure {code} not found")
        { }

        protected UnitMeasureNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}

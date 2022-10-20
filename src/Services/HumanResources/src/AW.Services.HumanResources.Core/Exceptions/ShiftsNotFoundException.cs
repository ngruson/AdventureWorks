using AW.Services.Infrastructure;
using System.Runtime.Serialization;

namespace AW.Services.HumanResources.Core.Exceptions
{
    [Serializable]
    public class ShiftsNotFoundException : DomainException
    {
        public ShiftsNotFoundException()
            : base($"No shifts found")
        { }

        protected ShiftsNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}
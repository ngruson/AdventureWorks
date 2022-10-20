using AW.Services.Infrastructure;
using System.Runtime.Serialization;

namespace AW.Services.HumanResources.Core.Exceptions
{
    [Serializable]
    public class ShiftNotFoundException : DomainException
    {
        public ShiftNotFoundException(string name)
            : base($"Shift '{name}' not found")
        { }

        protected ShiftNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}
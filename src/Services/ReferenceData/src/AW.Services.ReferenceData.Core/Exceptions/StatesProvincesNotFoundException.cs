using AW.Services.Infrastructure;
using System;
using System.Runtime.Serialization;

namespace AW.Services.ReferenceData.Core.Exceptions
{
    [Serializable]
    public class StatesProvincesNotFoundException : DomainException
    {
        public StatesProvincesNotFoundException()
            : base($"States/provinces not found")
        { }

        protected StatesProvincesNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}
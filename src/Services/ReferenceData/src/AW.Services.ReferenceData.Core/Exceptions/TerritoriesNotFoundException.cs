using AW.Services.Infrastructure;
using System;
using System.Runtime.Serialization;

namespace AW.Services.ReferenceData.Core.Exceptions
{
    [Serializable]
    public class TerritoriesNotFoundException : DomainException
    {
        public TerritoriesNotFoundException()
            : base($"Territories not found")
        { }

        protected TerritoriesNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}
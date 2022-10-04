using AW.Services.Infrastructure;
using System;
using System.Runtime.Serialization;

namespace AW.Services.ReferenceData.Core.Exceptions
{
    [Serializable]
    public class CountriesNotFoundException : DomainException
    {
        public CountriesNotFoundException()
            : base($"Countries not found")
        { }

        protected CountriesNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}
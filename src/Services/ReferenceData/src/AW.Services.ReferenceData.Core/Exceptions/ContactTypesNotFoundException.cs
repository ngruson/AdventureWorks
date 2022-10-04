using AW.Services.Infrastructure;
using System;
using System.Runtime.Serialization;

namespace AW.Services.ReferenceData.Core.Exceptions
{
    [Serializable]
    public class ContactTypesNotFoundException : DomainException
    {
        public ContactTypesNotFoundException()
            : base($"Contact types not found")
        { }

        protected ContactTypesNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}
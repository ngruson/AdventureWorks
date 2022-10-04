using AW.Services.Infrastructure;
using System;
using System.Runtime.Serialization;

namespace AW.Services.ReferenceData.Core.Exceptions
{
    [Serializable]
    public class AddressTypesNotFoundException : DomainException
    {
        public AddressTypesNotFoundException()
            : base($"Address types not found")
        { }

        protected AddressTypesNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}
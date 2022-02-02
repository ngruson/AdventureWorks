using System;
using System.Runtime.Serialization;

namespace AW.Services.Infrastructure
{
    [Serializable]
    public class DomainException : Exception, ISerializable
    {
        public DomainException()
        { }

        public DomainException(string message)
            : base(message)
        { }

        public DomainException(string message, Exception innerException)
            : base(message, innerException)
        { }

        protected DomainException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            :base(serializationInfo, streamingContext)
        {
        }
    }
}
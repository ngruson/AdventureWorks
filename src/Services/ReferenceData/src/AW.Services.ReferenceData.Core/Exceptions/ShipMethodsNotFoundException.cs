using AW.Services.Infrastructure;
using System;
using System.Runtime.Serialization;

namespace AW.Services.ReferenceData.Core.Exceptions
{
    [Serializable]
    public class ShipMethodsNotFoundException : DomainException
    {
        public ShipMethodsNotFoundException()
            : base($"Ship methods not found")
        { }

        protected ShipMethodsNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}
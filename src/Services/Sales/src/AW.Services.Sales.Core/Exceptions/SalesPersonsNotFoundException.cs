using AW.Services.Infrastructure;
using System;
using System.Runtime.Serialization;

namespace AW.Services.Sales.Core.Exceptions
{
    [Serializable]
    public class SalesPersonsNotFoundException : DomainException
    {
        public SalesPersonsNotFoundException()
            : base($"Sales persons not found")
        { }

        protected SalesPersonsNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}
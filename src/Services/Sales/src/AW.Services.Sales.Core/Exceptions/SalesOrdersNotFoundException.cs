using AW.Services.Infrastructure;
using System;
using System.Runtime.Serialization;

namespace AW.Services.Sales.Core.Exceptions
{
    [Serializable]
    public class SalesOrdersNotFoundException : DomainException
    {
        public SalesOrdersNotFoundException()
            : base($"Sales orders not found")
        { }

        protected SalesOrdersNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}
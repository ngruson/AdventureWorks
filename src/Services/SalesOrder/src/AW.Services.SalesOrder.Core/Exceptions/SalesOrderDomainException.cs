using System;
using System.Runtime.Serialization;

namespace AW.Services.SalesOrder.Core.Exceptions
{
    [Serializable]
    public class SalesOrderDomainException : Exception
    {
        public SalesOrderDomainException()
        { }

        public SalesOrderDomainException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        public SalesOrderDomainException(string message)
            : base(message)
        { }

        public SalesOrderDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
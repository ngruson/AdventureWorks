using System;
using System.Runtime.Serialization;

namespace AW.Services.Sales.Core.Exceptions
{
    [Serializable]
    public class SalesOrderNotFoundException : SalesOrderDomainException
    {
        public SalesOrderNotFoundException()
        { }

        protected SalesOrderNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        public SalesOrderNotFoundException(string message)
            : base(message)
        { }

        public SalesOrderNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public override string Message => "Sales order not found";
    }
}
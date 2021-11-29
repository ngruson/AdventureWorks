using System;

namespace AW.Services.SalesOrder.Core.Exceptions
{
    public class SalesOrderDomainException : Exception
    {
        public SalesOrderDomainException()
        { }

        public SalesOrderDomainException(string message)
            : base(message)
        { }

        public SalesOrderDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
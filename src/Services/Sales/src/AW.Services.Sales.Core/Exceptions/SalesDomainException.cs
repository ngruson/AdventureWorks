using System;
using System.Runtime.Serialization;

namespace AW.Services.Sales.Core.Exceptions
{
    [Serializable]
    public class SalesDomainException : Exception
    {
        public SalesDomainException()
        { }

        protected SalesDomainException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        public SalesDomainException(string message)
            : base(message)
        { }

        public SalesDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
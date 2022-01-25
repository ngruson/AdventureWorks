using System;
using System.Runtime.Serialization;

namespace AW.Services.Sales.Core.Exceptions
{
    [Serializable]
    public class CustomerNotFoundException : SalesDomainException
    {
        public CustomerNotFoundException()
        { }

        protected CustomerNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        public CustomerNotFoundException(string message)
            : base(message)
        { }

        public CustomerNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public override string Message => "Customer not found";
    }
}
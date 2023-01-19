using AW.Services.Infrastructure;
using System.Runtime.Serialization;

namespace AW.Services.Sales.Core.Exceptions
{
    public class DuplicateSalesOrderException : DomainException
    {
        public DuplicateSalesOrderException(string salesOrderNumber)
            : base($"Duplicating sales order {salesOrderNumber} failed")
        { }

        protected DuplicateSalesOrderException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}
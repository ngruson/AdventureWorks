using AW.Services.Infrastructure;
using System.Runtime.Serialization;

namespace AW.Services.Sales.Core.Exceptions
{
    [Serializable]
    public class SalesOrderNotFoundException : DomainException
    {
        public SalesOrderNotFoundException(string salesOrderNumber)
            : base($"Sales order {salesOrderNumber} not found")
        { }

        protected SalesOrderNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}

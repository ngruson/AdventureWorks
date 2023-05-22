using AW.Services.Infrastructure;
using System.Runtime.Serialization;

namespace AW.Services.HumanResources.Core.Exceptions
{
    [Serializable]
    public class EmployeeNotFoundException : DomainException
    {
        public EmployeeNotFoundException(Guid objectId)
            : base($"Employee {objectId} not found")
        { }

        protected EmployeeNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}

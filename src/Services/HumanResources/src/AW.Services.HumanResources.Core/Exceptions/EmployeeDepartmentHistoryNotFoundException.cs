using System.Runtime.Serialization;
using AW.Services.Infrastructure;

namespace AW.Services.HumanResources.Core.Exceptions
{
    [Serializable]
    public class EmployeeDepartmentHistoryNotFoundException : DomainException
    {
        public EmployeeDepartmentHistoryNotFoundException(Guid objectId)
            : base($"Department history {objectId} not found")
        { }

        protected EmployeeDepartmentHistoryNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}

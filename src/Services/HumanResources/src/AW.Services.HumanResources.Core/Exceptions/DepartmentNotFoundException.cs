using AW.Services.Infrastructure;
using System.Runtime.Serialization;

namespace AW.Services.HumanResources.Core.Exceptions
{
    [Serializable]
    public class DepartmentNotFoundException : DomainException
    {
        public DepartmentNotFoundException(Guid objectId)
            : base($"Department '{objectId}' not found")
        { }

        protected DepartmentNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}

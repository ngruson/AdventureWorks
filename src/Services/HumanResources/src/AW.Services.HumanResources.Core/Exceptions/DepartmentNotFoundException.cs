using AW.Services.Infrastructure;
using System.Runtime.Serialization;

namespace AW.Services.HumanResources.Core.Exceptions
{
    [Serializable]
    public class DepartmentNotFoundException : DomainException
    {
        public DepartmentNotFoundException(string? name)
            : base($"Department '{name}' not found")
        { }

        protected DepartmentNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}

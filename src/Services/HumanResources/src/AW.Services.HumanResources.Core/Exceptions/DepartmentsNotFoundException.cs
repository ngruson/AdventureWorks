using AW.Services.Infrastructure;
using System.Runtime.Serialization;

namespace AW.Services.HumanResources.Core.Exceptions
{
    [Serializable]
    public class DepartmentsNotFoundException : DomainException
    {
        public DepartmentsNotFoundException()
            : base($"No departments found")
        { }

        protected DepartmentsNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}
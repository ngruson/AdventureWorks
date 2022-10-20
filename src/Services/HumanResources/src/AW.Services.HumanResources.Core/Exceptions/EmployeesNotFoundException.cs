using AW.Services.Infrastructure;
using System.Runtime.Serialization;

namespace AW.Services.HumanResources.Core.Exceptions
{
    [Serializable]
    public class EmployeesNotFoundException : DomainException
    {
        public EmployeesNotFoundException()
            : base($"No employees found")
        { }

        protected EmployeesNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}
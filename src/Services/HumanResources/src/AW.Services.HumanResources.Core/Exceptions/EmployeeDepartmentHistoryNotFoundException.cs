using System.Runtime.Serialization;
using AW.Services.Infrastructure;

namespace AW.Services.HumanResources.Core.Exceptions
{
    [Serializable]
    public class EmployeeDepartmentHistoryNotFoundException : DomainException
    {
        public EmployeeDepartmentHistoryNotFoundException(string loginID, string? departmentName, string? shiftName, DateTime startDate)
            : base($"Department history not found for employee {loginID}, " +
                  $"department {departmentName}, " +
                  $"shift {shiftName}, " +
                  $"start date {startDate.ToShortDateString()}"
            )
        { }

        protected EmployeeDepartmentHistoryNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}

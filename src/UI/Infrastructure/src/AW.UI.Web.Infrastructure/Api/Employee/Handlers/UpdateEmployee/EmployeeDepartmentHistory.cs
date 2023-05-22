using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Infrastructure.Api.Employee.Handlers.UpdateEmployee
{
    public class EmployeeDepartmentHistory : IMapFrom<GetEmployee.EmployeeDepartmentHistory>
    {
        public Guid ObjectId { get; set; }
        public Department? Department { get; set; }
        public Shift? Shift { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}

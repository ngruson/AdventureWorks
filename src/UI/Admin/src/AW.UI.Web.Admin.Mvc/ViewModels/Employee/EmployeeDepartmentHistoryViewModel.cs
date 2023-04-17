using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Employee
{
    public class EmployeeDepartmentHistoryViewModel : IMapFrom<SharedKernel.Employee.Handlers.GetEmployees.EmployeeDepartmentHistory>
    {
        public DepartmentViewModel? Department { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}

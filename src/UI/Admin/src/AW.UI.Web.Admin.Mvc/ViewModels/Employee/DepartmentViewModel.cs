using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Employee
{
    public class DepartmentViewModel : IMapFrom<SharedKernel.Employee.Handlers.GetEmployees.Department>
    {
        public string? Name { get; set; }

        public string? GroupName { get; set; }
    }
}

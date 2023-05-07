using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Employee
{
    public class EmployeeDepartmentHistoryViewModel : IMapFrom<Infrastructure.Api.Employee.Handlers.GetEmployees.EmployeeDepartmentHistory>
    {
        public DepartmentViewModel? Department { get; set; }
        public ShiftViewModel? Shift { get; set; }

        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End date")]
        public DateTime? EndDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Infrastructure.Api.Employee.Handlers.GetEmployees.EmployeeDepartmentHistory, EmployeeDepartmentHistoryViewModel>();
            profile.CreateMap<Infrastructure.Api.Employee.Handlers.GetEmployee.EmployeeDepartmentHistory, EmployeeDepartmentHistoryViewModel>();
        }
    }
}

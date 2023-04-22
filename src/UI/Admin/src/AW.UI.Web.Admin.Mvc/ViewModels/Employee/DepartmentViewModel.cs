using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Employee
{
    public class DepartmentViewModel : IMapFrom<SharedKernel.Employee.Handlers.GetEmployees.Department>
    {
        [Display(Name = "Department")]
        public string? Name { get; set; }

        public string? GroupName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SharedKernel.Employee.Handlers.GetEmployees.Department, DepartmentViewModel>();
            profile.CreateMap<SharedKernel.Employee.Handlers.GetEmployee.Department, DepartmentViewModel>();
        }
    }
}

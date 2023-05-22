using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Employee
{
    public class DepartmentViewModel : IMapFrom<Infrastructure.Api.Employee.Handlers.GetEmployees.Department>
    {
        [Display(Name = "Department")]
        public Guid ObjectId { get; set; }

        [Display(Name = "Department")]
        public string? Name { get; set; }

        public string? GroupName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Infrastructure.Api.Employee.Handlers.GetEmployees.Department, DepartmentViewModel>();
            profile.CreateMap<Infrastructure.Api.Employee.Handlers.GetEmployee.Department, DepartmentViewModel>();
            profile.CreateMap<DepartmentViewModel, Infrastructure.Api.Employee.Handlers.UpdateEmployee.Department>()
                .ReverseMap();
        }
    }
}

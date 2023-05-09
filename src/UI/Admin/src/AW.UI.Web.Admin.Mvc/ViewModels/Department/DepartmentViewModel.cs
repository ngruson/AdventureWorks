using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Department
{
    public class DepartmentViewModel : IMapFrom<Infrastructure.Api.Department.Handlers.GetDepartments.Department>
    {
        public Guid ObjectId { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Group name")]
        public string? GroupName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Infrastructure.Api.Department.Handlers.GetDepartments.Department, DepartmentViewModel>();
            profile.CreateMap<Infrastructure.Api.Department.Handlers.GetDepartment.Department, DepartmentViewModel>();
            profile.CreateMap<DepartmentViewModel, Infrastructure.Api.Department.Handlers.CreateDepartment.Department>();
            profile.CreateMap<DepartmentViewModel, Infrastructure.Api.Department.Handlers.UpdateDepartment.Department>();
        }
    }
}

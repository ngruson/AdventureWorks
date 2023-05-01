using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Department
{
    public class DepartmentViewModel : IMapFrom<SharedKernel.Department.Handlers.GetDepartments.Department>
    {
        [Display(Name = "Name")]
        [Required]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Group name")]
        public string? GroupName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SharedKernel.Department.Handlers.GetDepartments.Department, DepartmentViewModel>();
            profile.CreateMap<SharedKernel.Department.Handlers.GetDepartment.Department, DepartmentViewModel>();
            profile.CreateMap<DepartmentViewModel, SharedKernel.Department.Handlers.CreateDepartment.Department>();
        }
    }
}

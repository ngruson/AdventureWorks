using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Department
{
    public class EditDepartmentDepartmentViewModel : IMapFrom<SharedKernel.Department.Handlers.UpdateDepartment.Department>
    {
        public string? Name { get; set; }

        public string? GroupName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SharedKernel.Department.Handlers.UpdateDepartment.Department, EditDepartmentDepartmentViewModel>()
                .ReverseMap();
        }
    }
}

using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.SharedKernel.Department.Handlers.UpdateDepartment
{
    public class Department : IMapFrom<GetDepartment.Department> 
    {
        public string? Name { get; set; }

        public string? GroupName { get; set; }
    }
}

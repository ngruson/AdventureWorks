using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Infrastructure.Api.Department.Handlers.UpdateDepartment
{
    public class Department : IMapFrom<GetDepartment.Department>
    {
        public string? Name { get; set; }

        public string? GroupName { get; set; }
    }
}

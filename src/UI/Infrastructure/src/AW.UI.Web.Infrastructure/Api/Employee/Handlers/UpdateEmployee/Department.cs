using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Infrastructure.Api.Employee.Handlers.UpdateEmployee
{
    public class Department : IMapFrom<GetEmployee.Department>
    {
        public Guid ObjectId { get; set; }
        public string? Name { get; set; }

        public string? GroupName { get; set; }
    }
}

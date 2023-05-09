using AW.SharedKernel.AutoMapper;

namespace AW.Services.HumanResources.Core.Handlers.GetDepartments
{
    public class Department : IMapFrom<Entities.Department>
    {
        public Guid ObjectId { get; set; }
        public string? Name { get; set; }

        public string? GroupName { get; set; }
    }
}

using AW.SharedKernel.AutoMapper;

namespace AW.Services.HumanResources.Core.Handlers.GetAllEmployees
{
    public class Department : IMapFrom<Entities.Department>
    {
        public string? Name { get; set; }

        public string? GroupName { get; set; }
    }
}
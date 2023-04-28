using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.HumanResources.Core.Handlers.UpdateDepartment
{
    public class Department : IMapFrom<Entities.Department>
    {
        public string? Name { get; set; }

        public string? GroupName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Department, Department>()
                .ReverseMap();
        }
    }
}

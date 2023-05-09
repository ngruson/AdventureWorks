using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.HumanResources.Core.Handlers.CreateDepartment
{
    public class CreatedDepartment : IMapFrom<Entities.Department>
    {
        public Guid ObjectId { get; set; }
        public string? Name { get; set; }

        public string? GroupName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Department, CreatedDepartment>();
        }
    }
}

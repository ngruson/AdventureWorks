using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.HumanResources.Core.Handlers.CreateEmployee
{
    public class EmployeeDepartmentHistory : IMapFrom<Entities.EmployeeDepartmentHistory>
    {
        public Guid Department { get; set; }
        public Guid Shift { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.EmployeeDepartmentHistory, EmployeeDepartmentHistory>()
                .ForMember(_ => _.Department, opt => opt.MapFrom(src => src.Department!.ObjectId))
                .ForMember(_ => _.Shift, opt => opt.MapFrom(src => src.Shift!.ObjectId))
                .ReverseMap();
        }
    }
}

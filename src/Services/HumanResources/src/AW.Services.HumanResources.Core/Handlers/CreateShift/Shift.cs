using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.HumanResources.Core.Handlers.CreateShift
{
    public class Shift : IMapFrom<Entities.Shift>
    {
        public string? Name { get; set; }

        public string? StartTime { get; set; }

        public string? EndTime { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Shift, Shift>()
                .ReverseMap();
        }
    }
}

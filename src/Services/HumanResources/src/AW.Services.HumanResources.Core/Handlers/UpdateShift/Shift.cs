using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.HumanResources.Core.Handlers.UpdateShift
{
    public class Shift : IMapFrom<Entities.Shift>
    {
        public Guid ObjectId { get; set; }
        public string? Name { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Shift, Shift>()
                .ReverseMap();
        }
    }
}

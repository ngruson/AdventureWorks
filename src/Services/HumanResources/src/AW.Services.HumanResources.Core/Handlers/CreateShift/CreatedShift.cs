using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.HumanResources.Core.Handlers.CreateShift
{
    public class CreatedShift : IMapFrom<Entities.Shift>
    {
        public Guid ObjectId { get; set; }
        public string? Name { get; set; }

        public string? StartTime { get; set; }

        public string? EndTime { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Shift, CreatedShift>();
        }
    }
}

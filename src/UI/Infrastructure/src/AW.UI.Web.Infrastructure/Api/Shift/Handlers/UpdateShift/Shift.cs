using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Infrastructure.Api.Shift.Handlers.UpdateShift
{
    public class Shift : IMapFrom<GetShift.Shift>
    {
        public Guid ObjectId { get; set; }
        public string? Name { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}

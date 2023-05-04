using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.SharedKernel.Shift.Handlers.UpdateShift
{
    public class Shift : IMapFrom<GetShift.Shift>
    {
        public string? Name { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}

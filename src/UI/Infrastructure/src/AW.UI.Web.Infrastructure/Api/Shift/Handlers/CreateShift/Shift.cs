namespace AW.UI.Web.Infrastructure.Api.Shift.Handlers.CreateShift
{
    public class Shift
    {
        public string? Name { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}

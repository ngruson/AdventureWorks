namespace AW.UI.Web.Infrastructure.Api.Shift.Handlers.CreateShift
{
    public class CreatedShift
    {
        public Guid ObjectId { get; set; }
        public string? Name { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}

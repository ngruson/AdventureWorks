namespace AW.UI.Web.Infrastructure.Api.Employee.Handlers.GetEmployee
{
    public class Shift
    {
        public Guid ObjectId { get; set; }
        public string? Name { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}

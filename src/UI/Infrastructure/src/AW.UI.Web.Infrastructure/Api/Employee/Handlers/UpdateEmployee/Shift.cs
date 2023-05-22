using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Infrastructure.Api.Employee.Handlers.UpdateEmployee
{
    public class Shift : IMapFrom<GetEmployee.Shift>
    {
        public Guid ObjectId { get; set; }
        public string? Name { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}

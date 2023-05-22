using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Employee.Handlers.UpdateDepartmentHistory
{
    public class UpdateDepartmentHistoryCommand : IRequest
    {
        public Guid ObjectId { get; set; }
        public Guid Employee { get; set; }
        public Guid Department { get; set; }
        public Guid Shift { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

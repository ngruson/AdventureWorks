using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Employee.Handlers.AddDepartmentHistory
{
    public class AddDepartmentHistoryCommand : IRequest
    {
        public Guid Employee { get; set; }
        public Guid Department { get; set; }
        public Guid Shift { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

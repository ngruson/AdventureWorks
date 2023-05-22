using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Employee.Handlers.DeleteDepartmentHistory
{
    public class DeleteDepartmentHistoryCommand : IRequest
    {
        public Guid Employee { get; set; }
        public Guid ObjectId { get; set; }
    }
}

using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Employee.Handlers.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest
    {
        public DeleteEmployeeCommand(Guid objectId)
        {
            ObjectId = objectId;
        }
        public Guid ObjectId { get; set; }
    }
}

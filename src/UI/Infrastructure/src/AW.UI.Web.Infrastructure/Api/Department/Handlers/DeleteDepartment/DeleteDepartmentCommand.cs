using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Department.Handlers.DeleteDepartment
{
    public class DeleteDepartmentCommand : IRequest
    {
        public DeleteDepartmentCommand(Guid objectId)
        {
            ObjectId = objectId;
        }
        public Guid ObjectId { get; set; }
    }
}

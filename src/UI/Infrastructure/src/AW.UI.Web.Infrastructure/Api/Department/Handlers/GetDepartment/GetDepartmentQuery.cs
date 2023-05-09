using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Department.Handlers.GetDepartment
{
    public class GetDepartmentQuery : IRequest<Department?>
    {
        public GetDepartmentQuery(Guid objectId)
        {
            ObjectId = objectId;
        }
        public Guid ObjectId { get; set; }
    }
}

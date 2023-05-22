using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Employee.Handlers.GetEmployee
{
    public class GetEmployeeQuery : IRequest<Employee>
    {
        public GetEmployeeQuery(Guid objectId)
        {
            ObjectId = objectId;
        }
        public Guid ObjectId { get; set; }
    }
}

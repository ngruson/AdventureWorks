using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Department.Handlers.UpdateDepartment
{
    public class UpdateDepartmentCommand : IRequest
    {
        public UpdateDepartmentCommand(Department department)
        {
            Department = department;
        }

        public Department Department { get; set; }
    }
}

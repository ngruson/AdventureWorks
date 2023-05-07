using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Department.Handlers.UpdateDepartment
{
    public class UpdateDepartmentCommand : IRequest
    {
        public UpdateDepartmentCommand(string key, Department department)
        {
            Key = key;
            Department = department;
        }

        public string Key { get; set; }
        public Department Department { get; set; }
    }
}

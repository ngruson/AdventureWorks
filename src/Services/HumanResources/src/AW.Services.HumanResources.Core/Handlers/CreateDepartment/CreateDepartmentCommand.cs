using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.CreateDepartment
{
    public class CreateDepartmentCommand : IRequest<Department>
    {
        public CreateDepartmentCommand(Department? department)
        {
            Department = department;
        }
        public Department? Department { get; set; }
    }
}

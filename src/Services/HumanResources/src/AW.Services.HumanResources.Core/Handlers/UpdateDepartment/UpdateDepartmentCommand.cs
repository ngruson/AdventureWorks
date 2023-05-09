using Ardalis.Result;
using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.UpdateDepartment
{
    public class UpdateDepartmentCommand : IRequest<Result<Department>>
    {
        public UpdateDepartmentCommand(Department department)
        {
            Department = department;
        }

        public Department Department { get; set; }
    }
}

using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.UpdateDepartment
{
    public class UpdateDepartmentCommand : IRequest<Department>
    {
        public UpdateDepartmentCommand(string key, Department department)
        {
            Key = key;
            Department = department;
        }

        public string Key { get; set; }
        public Department? Department { get; set; }
    }
}

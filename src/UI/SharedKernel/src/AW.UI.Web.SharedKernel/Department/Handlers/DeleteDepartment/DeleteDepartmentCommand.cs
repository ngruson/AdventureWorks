using MediatR;

namespace AW.UI.Web.SharedKernel.Department.Handlers.DeleteDepartment
{
    public class DeleteDepartmentCommand : IRequest
    {
        public DeleteDepartmentCommand(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}

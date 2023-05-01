using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.DeleteDepartment
{
    public class DeleteDepartmentCommand : IRequest
    {
        public string? Name { get; set; }
    }
}

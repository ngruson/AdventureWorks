using Ardalis.Result;
using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.DeleteDepartment
{
    public class DeleteDepartmentCommand : IRequest<Result>
    {
        public DeleteDepartmentCommand(Guid objectId)
        {
            ObjectId = objectId;
        }
        public Guid ObjectId { get; set; }
    }
}

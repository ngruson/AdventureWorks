using Ardalis.Result;
using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest<Result>
    {
        public DeleteEmployeeCommand(Guid objectId)
        {
            ObjectId = objectId;
        }
        public Guid ObjectId { get; set; }
    }
}

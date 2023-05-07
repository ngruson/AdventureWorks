using Ardalis.Result;
using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.DeleteShift
{
    public class DeleteShiftCommand : IRequest<Result>
    {
        public DeleteShiftCommand(Guid objectId)
        {
            ObjectId = objectId;
        }
        public Guid ObjectId { get; set; }
    }
}

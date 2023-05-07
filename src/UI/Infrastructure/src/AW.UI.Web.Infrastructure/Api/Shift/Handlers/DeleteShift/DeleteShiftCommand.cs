using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Shift.Handlers.DeleteShift
{
    public class DeleteShiftCommand : IRequest
    {
        public DeleteShiftCommand(Guid objectId)
        {
            ObjectId = objectId;
        }
        public Guid ObjectId { get; set; }
    }
}

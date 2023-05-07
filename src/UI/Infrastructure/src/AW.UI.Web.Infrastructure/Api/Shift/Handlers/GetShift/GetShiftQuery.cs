using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Shift.Handlers.GetShift
{
    public class GetShiftQuery : IRequest<Shift>
    {
        public GetShiftQuery(Guid objectId)
        {
            ObjectId = objectId;
        }
        public Guid ObjectId { get; set; }
    }
}

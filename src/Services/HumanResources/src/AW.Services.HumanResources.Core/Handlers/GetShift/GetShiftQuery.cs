using Ardalis.Result;
using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.GetShift
{
    public class GetShiftQuery : IRequest<Result<Shift>>
    {
        public GetShiftQuery(Guid objectId)
        {
            ObjectId = objectId;
        }

        public Guid ObjectId { get; set; }

        
    }
}

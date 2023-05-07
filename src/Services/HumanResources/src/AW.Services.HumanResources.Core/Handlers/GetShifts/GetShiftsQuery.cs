using Ardalis.Result;
using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.GetShifts
{
    public class GetShiftsQuery : IRequest<Result<List<Shift>>>
    {
    }
}

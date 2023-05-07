using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Shift.Handlers.GetShifts
{
    public class GetShiftsQuery : IRequest<List<Shift>>
    {
    }
}

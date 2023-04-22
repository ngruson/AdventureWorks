using MediatR;

namespace AW.UI.Web.SharedKernel.Shift.Handlers.GetShifts
{
    public class GetShiftsQuery : IRequest<List<Shift>>
    {
    }
}

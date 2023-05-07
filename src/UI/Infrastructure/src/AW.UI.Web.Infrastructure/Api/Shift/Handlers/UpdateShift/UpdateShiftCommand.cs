using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Shift.Handlers.UpdateShift
{
    public class UpdateShiftCommand : IRequest
    {
        public UpdateShiftCommand(Shift shift)
        {
            Shift = shift;
        }

        public Shift Shift { get; set; }
    }
}

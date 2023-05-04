using MediatR;

namespace AW.UI.Web.SharedKernel.Shift.Handlers.CreateShift
{
    public class CreateShiftCommand : IRequest<Shift>
    {
        public CreateShiftCommand(Shift shift)
        {
            Shift = shift;
        }
        public Shift Shift { get; set; }
    }
}

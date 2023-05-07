using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Shift.Handlers.CreateShift
{
    public class CreateShiftCommand : IRequest<CreatedShift>
    {
        public CreateShiftCommand(Shift shift)
        {
            Shift = shift;
        }
        public Shift Shift { get; set; }
    }
}

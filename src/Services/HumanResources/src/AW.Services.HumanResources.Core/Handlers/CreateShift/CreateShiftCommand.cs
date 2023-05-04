using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.CreateShift
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

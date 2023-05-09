using Ardalis.Result;
using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.UpdateShift
{
    public class UpdateShiftCommand : IRequest<Result<Shift>>
    {
        public UpdateShiftCommand(Shift shift)
        {
            Shift = shift;
        }

        public Shift Shift { get; set; }

    }
}

using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.UpdateShift
{
    public class UpdateShiftCommand : IRequest<Shift>
    {
        public UpdateShiftCommand(string key, Shift shift)
        {
            Key = key;
            Shift = shift;
        }

        public string Key { get; set; }
        public Shift? Shift { get; set; }

    }
}

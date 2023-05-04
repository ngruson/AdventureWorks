using MediatR;

namespace AW.UI.Web.SharedKernel.Shift.Handlers.UpdateShift
{
    public class UpdateShiftCommand : IRequest
    {
        public UpdateShiftCommand(string key, Shift shift)
        {
            Key = key;
            Shift = shift;
        }

        public string Key { get; set; }
        public Shift Shift { get; set; }
    }
}

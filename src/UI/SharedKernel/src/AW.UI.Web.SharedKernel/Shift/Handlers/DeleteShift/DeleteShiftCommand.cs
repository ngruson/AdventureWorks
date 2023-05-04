using MediatR;

namespace AW.UI.Web.SharedKernel.Shift.Handlers.DeleteShift
{
    public class DeleteShiftCommand : IRequest
    {
        public DeleteShiftCommand(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}

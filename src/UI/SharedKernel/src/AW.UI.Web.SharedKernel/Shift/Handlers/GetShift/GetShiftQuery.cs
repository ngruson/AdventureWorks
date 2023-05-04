using MediatR;

namespace AW.UI.Web.SharedKernel.Shift.Handlers.GetShift
{
    public class GetShiftQuery : IRequest<Shift>
    {
        public GetShiftQuery(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}

using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.GetShift
{
    public class GetShiftQuery : IRequest<Shift>
    {
        public string? Name { get; set; }
    }
}

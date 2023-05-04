using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.DeleteShift
{
    public class DeleteShiftCommand : IRequest
    {
        public string? Name { get; set; }
    }
}

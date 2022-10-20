using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.GetShift
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
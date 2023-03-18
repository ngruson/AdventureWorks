using MediatR;

namespace AW.Services.Product.Core.Handlers.GetUnitMeasures
{
    public class GetUnitMeasuresQuery : IRequest<List<UnitMeasure>>
    {
    }
}

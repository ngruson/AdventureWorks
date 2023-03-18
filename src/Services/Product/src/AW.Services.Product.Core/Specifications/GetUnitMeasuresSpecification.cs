using Ardalis.Specification;

namespace AW.Services.Product.Core.Specifications
{
    public class GetUnitMeasuresSpecification : Specification<Entities.UnitMeasure>
    {
        public GetUnitMeasuresSpecification()
        {
            Query.OrderBy(_ => _.Name);
        }
    }
}

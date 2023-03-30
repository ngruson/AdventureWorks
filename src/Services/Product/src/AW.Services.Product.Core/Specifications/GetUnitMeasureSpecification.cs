using Ardalis.Specification;

namespace AW.Services.Product.Core.Specifications
{
    public class GetUnitMeasureSpecification : Specification<Entities.UnitMeasure>, ISingleResultSpecification<Entities.UnitMeasure>
    {
        public GetUnitMeasureSpecification(string code)
        {
            Query.Where(p => p.UnitMeasureCode == code);
        }
    }
}

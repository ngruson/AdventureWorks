using Ardalis.Specification;

namespace AW.Services.HumanResources.Core.Specifications
{
    public class GetShiftSpecification : Specification<Entities.Shift>, ISingleResultSpecification<Entities.Shift>
    {
        public GetShiftSpecification(string? name) : base()
        {
            Query
                .Where(c => c.Name == name);
        }
    }
}

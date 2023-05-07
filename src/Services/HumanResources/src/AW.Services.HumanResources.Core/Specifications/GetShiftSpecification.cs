using Ardalis.Specification;

namespace AW.Services.HumanResources.Core.Specifications
{
    public class GetShiftSpecification : Specification<Entities.Shift>, ISingleResultSpecification<Entities.Shift>
    {
        public GetShiftSpecification(Guid objectId) : base()
        {
            Query
                .Where(c => c.ObjectId == objectId);
        }
    }
}

using Ardalis.Specification;

namespace AW.Services.HumanResources.Core.Specifications
{
    public class GetEmployeeSpecification : Specification<Entities.Employee>, ISingleResultSpecification<Entities.Employee>
    {
        public GetEmployeeSpecification(Guid objectId) : base()
        {
            Query
                .Include(_ => _.DepartmentHistory)
                .ThenInclude(_ => _.Department);

            Query
                .Include(_ => _.DepartmentHistory)
                .ThenInclude(_ => _.Shift);

            Query
                .Where(c => c.ObjectId == objectId);

        }
    }
}

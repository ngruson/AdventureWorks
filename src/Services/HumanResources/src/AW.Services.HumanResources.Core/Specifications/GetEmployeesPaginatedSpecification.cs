using Ardalis.Specification;

namespace AW.Services.HumanResources.Core.Specifications
{
    public class GetEmployeesPaginatedSpecification : Specification<Entities.Employee>
    {
        public GetEmployeesPaginatedSpecification(int pageIndex, int pageSize) : base()
        {
            Query
                .Include(_ => _.DepartmentHistory)
                .ThenInclude(_ => _.Department);

            Query
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .OrderBy(c => c.LoginID);
        }
    }
}
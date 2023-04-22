using Ardalis.Specification;

namespace AW.Services.HumanResources.Core.Specifications
{
    public class GetEmployeesSpecification : Specification<Entities.Employee>
    {
        public GetEmployeesSpecification()
        {

            Query.Include(_ => _.DepartmentHistory)
                .ThenInclude(_ => _.Department);

            Query
                .Include(_ => _.DepartmentHistory)
                .ThenInclude(_ => _.Shift);

            Query.OrderBy(_ => _.Id);
        }
    }
}

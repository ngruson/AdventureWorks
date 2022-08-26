using Ardalis.Specification;

namespace AW.Services.HumanResources.Core.Specifications
{
    public class GetAllEmployeesSpecification : Specification<Entities.Employee>
    {
        public GetAllEmployeesSpecification()
        {

            Query.Include(_ => _.DepartmentHistory)
                .ThenInclude(_ => _.Department);

            Query.OrderBy(_ => _.Id);
        }
    }
}
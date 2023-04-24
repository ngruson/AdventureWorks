using Ardalis.Specification;

namespace AW.Services.HumanResources.Core.Specifications
{
    public class GetDepartmentsSpecification : Specification<Entities.Department>
    {
        public GetDepartmentsSpecification()
        {
            Query.OrderBy(_ => _.Name);
        }
    }
}

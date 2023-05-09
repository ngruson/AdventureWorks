using Ardalis.Specification;

namespace AW.Services.HumanResources.Core.Specifications
{
    public class GetDepartmentByNameSpecification : Specification<Entities.Department>, ISingleResultSpecification<Entities.Department>
    {
        public GetDepartmentByNameSpecification(string name) : base()
        {
            Query
                .Where(c => c.Name == name);
        }
    }
}

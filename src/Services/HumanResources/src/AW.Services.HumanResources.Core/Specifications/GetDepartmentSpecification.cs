using Ardalis.Specification;

namespace AW.Services.HumanResources.Core.Specifications
{
    public class GetDepartmentSpecification : Specification<Entities.Department>, ISingleResultSpecification<Entities.Department>
    {
        public GetDepartmentSpecification(string name) : base()
        {
            Query
                .Where(c => c.Name == name);
        }
    }
}
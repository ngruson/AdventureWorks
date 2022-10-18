using Ardalis.Specification;
using System.Linq;

namespace AW.Services.HumanResources.Core.Specifications
{
    public class GetEmployeeSpecification : Specification<Entities.Employee>, ISingleResultSpecification<Entities.Employee>
    {
        public GetEmployeeSpecification(string loginID) : base()
        {
            Query
                .Include(_ => _.DepartmentHistory)
                .ThenInclude(_ => _.Department);

            Query
                .Where(c => c.LoginID == loginID);

        }
    }
}
using Ardalis.Specification;

namespace AW.Services.HumanResources.Core.Specifications
{
    public class GetEmployeeByLoginIDSpecification : Specification<Entities.Employee>, ISingleResultSpecification<Entities.Employee>
    {
        public GetEmployeeByLoginIDSpecification(string loginID) : base()
        {
            Query
                .Include(_ => _.DepartmentHistory)
                .ThenInclude(_ => _.Department);

            Query
                .Include(_ => _.DepartmentHistory)
                .ThenInclude(_ => _.Shift);

            Query
                .Where(c => c.LoginID == loginID);

        }
    }
}

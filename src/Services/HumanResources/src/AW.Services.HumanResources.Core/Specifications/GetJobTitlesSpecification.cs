using Ardalis.Specification;

namespace AW.Services.HumanResources.Core.Specifications
{
    public class GetJobTitlesSpecification : Specification<Entities.Employee, string>
    {
        public GetJobTitlesSpecification() : base()
        {
            Query!.Select(c => c.JobTitle);
        }
    }
}

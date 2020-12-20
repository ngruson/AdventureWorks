using Ardalis.Specification;

namespace AW.Core.Application.Specifications
{
    public class GetPersonSpecification : Specification<Domain.Person.Person>
    {
        public GetPersonSpecification(
            string firstName,
            string middleName,
            string lastName
        ) : base()
        {
            Query
                .Where(p => p.FirstName == firstName 
                    && p.MiddleName == middleName
                    && p.LastName == lastName
                );
        }
    }
}
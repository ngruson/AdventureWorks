using Ardalis.Specification;
using AW.Domain.Person;

namespace AW.Persistence.EntityFramework.UnitTests.Specifications
{
    public class GetPersonsByLastNameSpecification : Specification<Person>
    {
        public GetPersonsByLastNameSpecification(string lastName)
        {
            Query
                .Where(p => p.LastName == lastName);
        }
    }
}
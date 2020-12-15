using Ardalis.Specification;
using AW.Domain.Person;

namespace AW.Persistence.EntityFrameworkCore.UnitTests.Specifications
{
    public class GetPersonsLastNameSpecification : Specification<Person, string>
    {

        public GetPersonsLastNameSpecification()
        {
            Query.Select(p => p.LastName);
        }
    }
}
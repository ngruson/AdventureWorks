using Ardalis.Specification;
using AW.Domain.Person;

namespace AW.Persistence.EntityFrameworkCore.UnitTests.Specifications
{
    public class GetPersonsLastNameWithoutSelectorSpecification : Specification<Person, string>
    {

        public GetPersonsLastNameWithoutSelectorSpecification()
        {
        }
    }
}
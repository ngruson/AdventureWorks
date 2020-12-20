using Ardalis.Specification;
using AW.Core.Domain.Person;

namespace AW.Persistence.EntityFramework.UnitTests.Specifications
{
    public class GetPersonsLastNameWithoutSelectorSpecification : Specification<Person, string>
    {

        public GetPersonsLastNameWithoutSelectorSpecification()
        {
        }
    }
}
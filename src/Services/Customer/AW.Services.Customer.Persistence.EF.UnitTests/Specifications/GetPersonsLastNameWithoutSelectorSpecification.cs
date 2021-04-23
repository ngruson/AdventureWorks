using Ardalis.Specification;
using AW.Services.Customer.Domain;

namespace AW.Services.Customer.Persistence.EF.UnitTests.Specifications
{
    public class GetPersonsLastNameWithoutSelectorSpecification : Specification<Person, string>
    {

        public GetPersonsLastNameWithoutSelectorSpecification()
        {
        }
    }
}
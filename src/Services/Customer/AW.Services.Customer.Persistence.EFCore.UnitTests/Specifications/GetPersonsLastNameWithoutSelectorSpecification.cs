using Ardalis.Specification;
using AW.Services.Customer.Domain;

namespace AW.Services.Customer.Persistence.EFCore.UnitTests.Specifications
{
    public class GetPersonsLastNameWithoutSelectorSpecification : Specification<Person, string>
    {

        public GetPersonsLastNameWithoutSelectorSpecification()
        {
        }
    }
}
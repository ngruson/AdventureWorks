using Ardalis.Specification;
using AW.Services.Customer.Domain;

namespace AW.Services.Customer.Persistence.EFCore.UnitTests.Specifications
{
    public class GetPersonsLastNameSpecification : Specification<Person, string>
    {

        public GetPersonsLastNameSpecification()
        {
            Query.Select(p => p.LastName);
        }
    }
}
using Ardalis.Specification;
using AW.Services.Customer.Domain;

namespace AW.Services.Customer.Persistence.EF.UnitTests.Specifications
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
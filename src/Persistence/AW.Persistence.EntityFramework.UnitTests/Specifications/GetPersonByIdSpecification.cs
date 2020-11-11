using Ardalis.Specification;
using AW.Domain.Person;

namespace AW.Persistence.EntityFramework.UnitTests.Specifications
{
    public class GetPersonByIdSpecification : Specification<Person>
    {
        public GetPersonByIdSpecification(int id)
        {
            Query
                .Where(p => p.Id == id);
        }
    }
}
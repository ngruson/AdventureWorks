using Ardalis.Specification;
using AW.Services.Customer.Domain;

namespace AW.Services.Customer.Persistence.EFCore.UnitTests.Specifications
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
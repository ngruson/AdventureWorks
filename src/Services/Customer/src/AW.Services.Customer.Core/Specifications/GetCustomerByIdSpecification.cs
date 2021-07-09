using Ardalis.Specification;

namespace AW.Services.Customer.Core.Specifications
{
    public class GetCustomerByIdSpecification : Specification<Entities.Customer>, ISingleResultSpecification
    {
        public GetCustomerByIdSpecification(int id)
        {
            Query
                .Where(p => p.Id == id);
        }
    }
}
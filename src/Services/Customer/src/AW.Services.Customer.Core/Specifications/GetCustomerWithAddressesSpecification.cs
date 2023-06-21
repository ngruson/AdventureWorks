using Ardalis.Specification;

namespace AW.Services.Customer.Core.Specifications
{
    public class GetCustomerWithAddressesSpecification : Specification<Entities.Customer>, ISingleResultSpecification<Entities.Customer>
    {
        public GetCustomerWithAddressesSpecification(Guid customerId)
        {
            Query.Include(c => c.Addresses)
                .ThenInclude(a => a.Address);

            Query
                .Where(c => c.ObjectId == customerId);
        }
    }
}

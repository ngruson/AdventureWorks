using Ardalis.Specification;

namespace AW.Services.Customer.Core.Specifications
{
    public class GetStoreCustomerSpecification : Specification<Entities.StoreCustomer>, ISingleResultSpecification<Entities.StoreCustomer>
    {
        public GetStoreCustomerSpecification(Guid objectId) : base()
        {
            Query
                .Where(c => c.ObjectId == objectId);
        }
    }
}

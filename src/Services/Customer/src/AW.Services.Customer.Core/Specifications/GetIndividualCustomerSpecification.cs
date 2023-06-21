using Ardalis.Specification;

namespace AW.Services.Customer.Core.Specifications
{
    public class GetIndividualCustomerSpecification : Specification<Entities.IndividualCustomer>, ISingleResultSpecification<Entities.IndividualCustomer>
    {
        public GetIndividualCustomerSpecification(Guid objectId) : base()
        {
            Query
                .Where(c => c.ObjectId == objectId);
        }
    }
}

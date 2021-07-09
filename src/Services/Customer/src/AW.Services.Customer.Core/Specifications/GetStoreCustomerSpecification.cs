using Ardalis.Specification;
using System.Linq;

namespace AW.Services.Customer.Core.Specifications
{
    public class GetStoreCustomerSpecification : Specification<Entities.StoreCustomer>, ISingleResultSpecification
    {
        public GetStoreCustomerSpecification(string accountNumber) : base()
        {
            Query
                .Where(c => c.AccountNumber == accountNumber);
        }
    }
}
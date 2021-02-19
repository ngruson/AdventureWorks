using Ardalis.Specification;
using System.Linq;

namespace AW.Services.Customer.Application.Specifications
{
    public class GetStoreCustomerSpecification : Specification<Domain.StoreCustomer>
    {
        public GetStoreCustomerSpecification(string accountNumber) : base()
        {
            Query
                .Where(c => c.AccountNumber == accountNumber);
        }
    }
}
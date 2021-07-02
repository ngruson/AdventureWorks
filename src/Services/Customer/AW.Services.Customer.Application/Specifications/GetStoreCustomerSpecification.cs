using Ardalis.Specification;
using System.Linq;

namespace AW.Services.Customer.Application.Specifications
{
    #if NETSTANDARD2_0
    public class GetStoreCustomerSpecification : Specification<Domain.StoreCustomer>
    #elif NETSTANDARD2_1
    public class GetStoreCustomerSpecification : Specification<Domain.StoreCustomer>, ISingleResultSpecification
    #endif
    {
        public GetStoreCustomerSpecification(string accountNumber) : base()
        {
            Query
                .Where(c => c.AccountNumber == accountNumber);
        }
    }
}
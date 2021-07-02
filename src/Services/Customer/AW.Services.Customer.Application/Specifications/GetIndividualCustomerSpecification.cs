using Ardalis.Specification;
using System.Linq;

namespace AW.Services.Customer.Application.Specifications
{
    #if NETSTANDARD2_0
    public class GetIndividualCustomerSpecification : Specification<Domain.IndividualCustomer>
    #elif NETSTANDARD2_1_OR_GREATER
    public class GetIndividualCustomerSpecification : Specification<Domain.IndividualCustomer>, ISingleResultSpecification
    #endif
    {
        public GetIndividualCustomerSpecification(string accountNumber) : base()
        {
            Query
                .Where(c => c.AccountNumber == accountNumber);
        }
    }
}
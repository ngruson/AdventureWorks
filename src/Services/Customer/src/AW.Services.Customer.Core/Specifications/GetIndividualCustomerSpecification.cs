using Ardalis.Specification;
using System.Linq;

namespace AW.Services.Customer.Core.Specifications
{
    public class GetIndividualCustomerSpecification : Specification<Entities.IndividualCustomer>, ISingleResultSpecification<Entities.IndividualCustomer>
    {
        public GetIndividualCustomerSpecification(string accountNumber) : base()
        {
            Query
                .Where(c => c.AccountNumber == accountNumber);
        }
    }
}
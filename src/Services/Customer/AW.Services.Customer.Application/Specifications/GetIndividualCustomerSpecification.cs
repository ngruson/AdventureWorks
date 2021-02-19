using Ardalis.Specification;
using System.Linq;

namespace AW.Services.Customer.Application.Specifications
{
    public class GetIndividualCustomerSpecification : Specification<Domain.IndividualCustomer>
    {
        public GetIndividualCustomerSpecification(string accountNumber) : base()
        {
            Query
                .Where(c => c.AccountNumber == accountNumber);
        }
    }
}
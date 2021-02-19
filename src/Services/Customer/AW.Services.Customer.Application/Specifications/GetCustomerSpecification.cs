using Ardalis.Specification;
using System.Linq;

namespace AW.Services.Customer.Application.Specifications
{
    public class GetCustomerSpecification : Specification<Domain.Customer>
    {
        public GetCustomerSpecification(string accountNumber) : base()
        {
            Query
                .Where(c => c.AccountNumber == accountNumber);

        }
    }
}
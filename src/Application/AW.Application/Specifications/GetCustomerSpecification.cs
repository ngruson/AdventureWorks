using Ardalis.Specification;
using AW.Domain.Sales;

namespace AW.Application.Specifications
{
    public class GetCustomerSpecification : Specification<Customer>
    {
        public GetCustomerSpecification(string accountNumber) : base()
        {
            Query
                .Where(c => c.AccountNumber == accountNumber);
                    
        }
    }
}
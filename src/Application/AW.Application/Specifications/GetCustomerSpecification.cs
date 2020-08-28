using Ardalis.Specification;

namespace AW.Application.Specifications
{
    public class GetCustomerSpecification : Specification<Domain.Sales.Customer>
    {
        public GetCustomerSpecification(string accountNumber) : base()
        {
            Query
                .Where(c => c.AccountNumber == accountNumber);
                    
        }
    }
}
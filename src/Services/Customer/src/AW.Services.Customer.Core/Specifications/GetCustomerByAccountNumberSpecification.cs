using Ardalis.Specification;

namespace AW.Services.Customer.Core.Specifications
{
    public class GetCustomerByAccountNumberSpecification : Specification<Entities.Customer>
    {
        public GetCustomerByAccountNumberSpecification(string accountNumber) : base()
        {
            Query
                .Where(c => c.AccountNumber == accountNumber);
        }
    }
}

using Ardalis.Specification;

namespace AW.Services.Customer.Core.Specifications
{
    public class GetCustomerAddressesSpecification : Specification<Entities.Customer>, ISingleResultSpecification
    {
        public GetCustomerAddressesSpecification(string accountNumber)
        {
            Query.Include(c => c.Addresses)
                .ThenInclude(a => a.Address);

            Query
                .Where(c => c.AccountNumber == accountNumber);
        }
    }
}
using Ardalis.Specification;
using AW.Services.Customer.Domain;
using System.Linq;

namespace AW.Services.Customer.Application.Specifications
{
    public class CountCustomersSpecification : Specification<Domain.Customer>
    {
        public CountCustomersSpecification(CustomerType? customerType, string territory, string accountNumber) : base()
        {
            Query
                .Where(c =>
                    (string.IsNullOrEmpty(territory) || c.Territory == territory) &&
                    (!customerType.HasValue || (customerType == CustomerType.Individual ?
                        c is IndividualCustomer : c is StoreCustomer)
                    ) &&
                    (string.IsNullOrEmpty(accountNumber) || c.AccountNumber == accountNumber)
                );
        }
    }
}
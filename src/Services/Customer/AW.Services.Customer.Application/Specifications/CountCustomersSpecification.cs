using Ardalis.Specification;
using AW.Services.Customer.Application.GetCustomers;
using AW.Services.Customer.Domain;
using System.Linq;

namespace AW.Services.Customer.Application.Specifications
{
    public class CountCustomersSpecification : Specification<Domain.Customer>
    {
        public CountCustomersSpecification(CustomerType? customerType, string territory) : base()
        {
            Query
                .Where(c =>
                    (string.IsNullOrEmpty(territory) || c.TerritoryName == territory) &&
                    (!customerType.HasValue || (customerType == CustomerType.Individual ?
                        c is IndividualCustomerDto : c is StoreCustomer)
                    )
                );
        }
    }
}
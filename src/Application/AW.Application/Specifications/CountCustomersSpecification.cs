using Ardalis.Specification;
using AW.Domain.Sales;

namespace AW.Application.Specifications
{
    public class CountCustomersSpecification : Specification<Customer>
    {
        public CountCustomersSpecification(CustomerType? customerType, string territory) : base()
        {
            Query
                .Where(c =>
                    (string.IsNullOrEmpty(territory) || c.SalesTerritory.Name == territory) &&
                    (!customerType.HasValue || (customerType == CustomerType.Individual ?
                        c.Person != null && c.Store == null
                        : customerType == CustomerType.Store && c.Store != null))
                );
        }
    }
}
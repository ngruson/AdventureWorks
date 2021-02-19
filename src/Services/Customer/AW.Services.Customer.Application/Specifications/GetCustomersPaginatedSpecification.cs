using Ardalis.Specification;
using AW.Services.Customer.Application.GetCustomers;
using AW.Services.Customer.Domain;
using System.Linq;

namespace AW.Services.Customer.Application.Specifications
{
    public class GetCustomersPaginatedSpecification : Specification<Domain.Customer>
    {
        public GetCustomersPaginatedSpecification(int pageIndex, int pageSize, CustomerType? customerType, string territory) : base()
        {
            Query
                .Where(c =>
                    (string.IsNullOrEmpty(territory) || c.TerritoryName == territory) &&
                    (!customerType.HasValue || (customerType == CustomerType.Individual ?
                        c is IndividualCustomer
                        : c is StoreCustomer)
                ))
                .Skip(pageIndex * pageSize)
                .Take(10)
                .OrderBy(c => c.AccountNumber);
        }
    }
}
using Ardalis.Specification;
using AW.Application.GetCustomers;
using AW.Domain.Sales;

namespace AW.Application.Specifications
{
    public class GetCustomersPaginatedSpecification : Specification<Customer>
    {
        public GetCustomersPaginatedSpecification(int pageIndex, int pageSize, CustomerType? customerType, string territory) : base()
        {
            Query
                .Where(c => 
                    (string.IsNullOrEmpty(territory) || c.SalesTerritory.Name == territory) &&
                    (!customerType.HasValue || (customerType == CustomerType.Individual ?
                        c.Person != null && c.Store == null
                        : customerType == CustomerType.Store && c.Store != null))
                )
                .Paginate(pageIndex * pageSize, pageSize)
                .OrderBy(c => c.AccountNumber);
        }
    }
}
using Ardalis.Specification;
using AW.Domain.Sales;

namespace AW.Application.Specifications
{
    public class GetCustomersPaginatedSpecification : Specification<Customer>
    {
        public GetCustomersPaginatedSpecification(int pageIndex, int pageSize, string territory) : base()
        {
            Query
                .Where(c => (string.IsNullOrEmpty(territory) || c.SalesTerritory.Name == territory))
                .Paginate(pageIndex * pageSize, pageSize)
                .OrderBy(c => c.AccountNumber);
        }
    }
}
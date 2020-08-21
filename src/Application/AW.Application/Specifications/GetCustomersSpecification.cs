using Ardalis.Specification;
using AW.Domain.Sales;

namespace AW.Application.Specifications
{
    public class GetCustomersSpecification : Specification<Customer>
    {
        public GetCustomersSpecification(string territory) : base()
        {
            Query
                .Where(c => (string.IsNullOrEmpty(territory) || c.SalesTerritory.Name == territory))
                .OrderBy(c => c.AccountNumber);
        }
    }
}
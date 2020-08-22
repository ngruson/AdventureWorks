using Ardalis.Specification;
using AW.Domain.Sales;

namespace AW.Application.Specifications
{
    public class GetSalesOrdersSpecification : Specification<SalesOrderHeader>
    {
        public GetSalesOrdersSpecification(CustomerType? customerType, string territory) : base()
        {
            Query
                .Where(so =>
                    (string.IsNullOrEmpty(territory) || so.SalesTerritory.Name == territory)
                    &&
                    (!customerType.HasValue || (customerType == CustomerType.Individual ?
                        so.Customer.Person != null && so.Customer.Store == null
                        : customerType == CustomerType.Store && so.Customer.Store != null))
                )
                .OrderBy(c => c.AccountNumber);
        }
    }
}
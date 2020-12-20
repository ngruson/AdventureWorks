using Ardalis.Specification;
using AW.Core.Domain.Sales;

namespace AW.Core.Application.Specifications
{
    public class CountSalesOrdersSpecification : Specification<SalesOrderHeader>
    {
        public CountSalesOrdersSpecification(CustomerType? customerType, string territory) : base()
        {
            Query
                .Where(so =>
                    (string.IsNullOrEmpty(territory) || so.SalesTerritory.Name == territory)
                    &&
                    (!customerType.HasValue || (customerType == CustomerType.Individual ?
                        so.Customer.Person != null && so.Customer.Store == null
                        : customerType == CustomerType.Store && so.Customer.Store != null))
                );
        }
    }
}
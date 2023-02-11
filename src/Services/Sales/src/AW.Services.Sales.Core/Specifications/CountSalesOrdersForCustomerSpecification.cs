using Ardalis.Specification;

namespace AW.Services.Sales.Core.Specifications
{
    public class CountSalesOrdersForCustomerSpecification : Specification<Entities.SalesOrder>
    {
        public CountSalesOrdersForCustomerSpecification(string customerNumber) : base()
        {
            Query.Where(_ => _.Customer!.CustomerNumber == customerNumber);
        }
    }
}
using Ardalis.Specification;
using System.Linq;

namespace AW.Services.SalesOrder.Core.Specifications
{
    public class CountSalesOrdersForCustomerSpecification : Specification<Entities.SalesOrder>
    {
        public CountSalesOrdersForCustomerSpecification(string customerNumber) : base()
        {
            Query.Where(_ => _.CustomerNumber == customerNumber);
        }
    }
}
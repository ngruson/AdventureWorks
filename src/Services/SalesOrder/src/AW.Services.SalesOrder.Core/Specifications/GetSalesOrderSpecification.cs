using Ardalis.Specification;

namespace AW.Services.SalesOrder.Core.Specifications
{
    public class GetSalesOrderSpecification : Specification<Entities.SalesOrder>, ISingleResultSpecification
    {
        public GetSalesOrderSpecification(string salesOrderNumber) : base()
        {
            Query
                .Where(c => c.SalesOrderNumber == salesOrderNumber);
        }
    }
}
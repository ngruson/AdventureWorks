using Ardalis.Specification;
using AW.Core.Domain.Sales;

namespace AW.Core.Application.Specifications
{
    public class GetSalesOrderSpecification : Specification<SalesOrderHeader>
    {
        public GetSalesOrderSpecification(string salesOrderNumber) : base()
        {
            Query
                .Where(c => c.SalesOrderNumber == salesOrderNumber);
        }
    }
}
using Ardalis.Specification;
using AW.Domain.Sales;

namespace AW.Application.Specifications
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
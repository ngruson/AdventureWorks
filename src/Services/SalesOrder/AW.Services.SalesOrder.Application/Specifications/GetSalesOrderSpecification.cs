using Ardalis.Specification;

namespace AW.Services.SalesOrder.Application.Specifications
{
    public class GetSalesOrderSpecification : Specification<Domain.SalesOrder>, ISingleResultSpecification
    {
        public GetSalesOrderSpecification(string salesOrderNumber) : base()
        {
            Query.Include(sp => sp.BillToAddress);
            Query.Include(sp => sp.ShipToAddress);
            Query.Include(so => so.OrderLines);
            Query.Include(so => so.SalesReasons);

            Query
                .Where(c => c.SalesOrderNumber == salesOrderNumber);
        }
    }
}
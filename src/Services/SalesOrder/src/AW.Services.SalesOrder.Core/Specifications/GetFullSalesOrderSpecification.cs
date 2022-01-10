using Ardalis.Specification;

namespace AW.Services.SalesOrder.Core.Specifications
{
    public class GetFullSalesOrderSpecification : Specification<Core.Entities.SalesOrder>, ISingleResultSpecification
    {
        public GetFullSalesOrderSpecification(string salesOrderNumber) : base()
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
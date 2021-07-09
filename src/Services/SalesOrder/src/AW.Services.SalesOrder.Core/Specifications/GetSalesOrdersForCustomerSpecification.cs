using Ardalis.Specification;

namespace AW.Services.SalesOrder.Core.Specifications
{
    public class GetSalesOrdersForCustomerSpecification : Specification<Core.Entities.SalesOrder>
    {
        public GetSalesOrdersForCustomerSpecification(string customerNumber)
        {
            Query.Include(sp => sp.BillToAddress);
            Query.Include(sp => sp.ShipToAddress);
            Query.Include(so => so.OrderLines);
            Query.Include(so => so.SalesReasons);

            Query
                .Where(so => so.CustomerNumber == customerNumber);
        }
    }
}
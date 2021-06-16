using Ardalis.Specification;

namespace AW.Services.SalesOrder.Application.Specifications
{
    public class GetSalesOrdersPaginatedSpecification : Specification<Domain.SalesOrder>
    {
        public GetSalesOrdersPaginatedSpecification(int pageIndex, int pageSize, string territory) : base()
        {
            Query.Include(sp => sp.BillToAddress);
            Query.Include(sp => sp.ShipToAddress);
            Query.Include(so => so.OrderLines);
            Query.Include(so => so.SalesReasons);

            Query
                .Where(so =>
                    (string.IsNullOrEmpty(territory) || so.Territory == territory)
                )
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .OrderByDescending(so => so.SalesOrderNumber);
        }
    }
}
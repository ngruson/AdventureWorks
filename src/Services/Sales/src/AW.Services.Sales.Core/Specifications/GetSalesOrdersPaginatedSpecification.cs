using Ardalis.Specification;

namespace AW.Services.Sales.Core.Specifications
{
    public class GetSalesOrdersPaginatedSpecification : Specification<Entities.SalesOrder>
    {
        public GetSalesOrdersPaginatedSpecification(int pageIndex, int pageSize, string? territory) : base()
        {
            Query.Include(_ => _.Customer);
            Query.Include("Customer.Person");
            Query.Include(_ => _.BillToAddress);
            Query.Include(_ => _.ShipToAddress);
            Query.Include(_ => _.OrderLines);
            Query.Include(_ => _.SalesReasons)
                .ThenInclude(_ => _.SalesReason);

            Query
                .Where(so =>
                    string.IsNullOrEmpty(territory) || so.Territory == territory
                )
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .OrderByDescending(so => so.SalesOrderNumber);
        }
    }
}

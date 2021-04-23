using Ardalis.Specification;
using AW.Services.SalesOrder.Domain;

namespace AW.Services.SalesOrder.Application.Specifications
{
    public class GetSalesOrdersPaginatedSpecification : Specification<Domain.SalesOrder>
    {
        public GetSalesOrdersPaginatedSpecification(int pageIndex, int pageSize, CustomerType? customerType, string territory) : base()
        {
            Query.Include(sp => sp.Customer);
            Query.Include(sp => sp.BillToAddress);
            Query.Include(sp => sp.ShipToAddress);
            Query.Include(so => so.OrderLines);
            Query.Include(so => so.SalesReasons);

            Query
                .Where(so =>
                    (string.IsNullOrEmpty(territory) || so.Territory == territory)
                    &&
                    (!customerType.HasValue || (customerType == CustomerType.Individual ? so.Customer is IndividualCustomer : 
                        customerType == CustomerType.Store && so.Customer is StoreCustomer)
                    )
                )
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .OrderByDescending(so => so.SalesOrderNumber);
        }
    }
}
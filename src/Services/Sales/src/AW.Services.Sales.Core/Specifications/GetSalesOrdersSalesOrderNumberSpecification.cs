using Ardalis.Specification;

namespace AW.Services.Sales.Core.Specifications
{
    public class GetSalesOrdersSalesOrderNumberSpecification : Specification<Entities.SalesOrder, string>
    {

        public GetSalesOrdersSalesOrderNumberSpecification()
        {
            Query.Select(p => p.SalesOrderNumber);
        }
    }
}
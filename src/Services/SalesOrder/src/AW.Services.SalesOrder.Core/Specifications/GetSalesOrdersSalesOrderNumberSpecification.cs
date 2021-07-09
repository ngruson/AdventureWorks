using Ardalis.Specification;

namespace AW.Services.SalesOrder.Core.Specifications
{
    public class GetSalesOrdersSalesOrderNumberSpecification : Specification<Core.Entities.SalesOrder, string>
    {

        public GetSalesOrdersSalesOrderNumberSpecification()
        {
            Query.Select(p => p.SalesOrderNumber);
        }
    }
}
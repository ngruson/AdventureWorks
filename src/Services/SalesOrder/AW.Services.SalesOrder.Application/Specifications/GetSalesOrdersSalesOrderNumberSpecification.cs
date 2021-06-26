using Ardalis.Specification;

namespace AW.Services.SalesOrder.Application.Specifications
{
    public class GetSalesOrdersSalesOrderNumberSpecification : Specification<Domain.SalesOrder, string>
    {

        public GetSalesOrdersSalesOrderNumberSpecification()
        {
            Query.Select(p => p.SalesOrderNumber);
        }
    }
}
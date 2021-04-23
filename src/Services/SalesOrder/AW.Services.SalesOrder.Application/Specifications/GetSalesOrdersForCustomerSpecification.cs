using Ardalis.Specification;

namespace AW.Services.SalesOrder.Application.Specifications
{
    public class GetSalesOrdersForCustomerSpecification : Specification<Domain.SalesOrder>
    {
        public GetSalesOrdersForCustomerSpecification(string customerNumber)
        {
            Query
                .Where(so => so.Customer.AccountNumber == customerNumber);
        }
    }
}
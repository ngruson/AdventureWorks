using Ardalis.Specification;
using AW.Services.SalesOrder.Domain;

namespace AW.Services.SalesOrder.Application.Specifications
{
    public class CountSalesOrdersSpecification : Specification<Domain.SalesOrder>
    {
        public CountSalesOrdersSpecification(string territory) : base()
        {
            Query
                .Where(so =>
                    (string.IsNullOrEmpty(territory) || so.Territory == territory)
                );
        }
    }
}
using Ardalis.Specification;

namespace AW.Services.SalesOrder.Core.Specifications
{
    public class CountSalesOrdersSpecification : Specification<Entities.SalesOrder>
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
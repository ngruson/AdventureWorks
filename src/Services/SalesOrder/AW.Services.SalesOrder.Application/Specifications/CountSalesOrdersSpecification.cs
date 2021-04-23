using Ardalis.Specification;
using AW.Services.SalesOrder.Domain;

namespace AW.Services.SalesOrder.Application.Specifications
{
    public class CountSalesOrdersSpecification : Specification<Domain.SalesOrder>
    {
        public CountSalesOrdersSpecification(CustomerType? customerType, string territory) : base()
        {
            Query
                .Where(so =>
                    (string.IsNullOrEmpty(territory) || so.Territory == territory)
                    &&
                    (!customerType.HasValue || (customerType == CustomerType.Individual ? so.Customer is IndividualCustomer :
                        customerType == CustomerType.Store && so.Customer is StoreCustomer)
                    )
                );
        }
    }
}
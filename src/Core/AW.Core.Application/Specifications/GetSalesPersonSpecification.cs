using Ardalis.Specification;

namespace AW.Core.Application.Specifications
{
    public class GetSalesPersonSpecification : Specification<Domain.Sales.SalesPerson>
    {
        public GetSalesPersonSpecification(string firstName, string middleName, string lastName) : base()
        {
            Query
                .Where(sp => sp.FirstName == firstName && sp.MiddleName == middleName && sp.LastName == lastName);
        }
    }
}
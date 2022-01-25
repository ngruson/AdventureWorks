using Ardalis.Specification;

namespace AW.Services.Sales.Core.Specifications
{
    public class GetSalesPersonsFullNameSpecification : Specification<Entities.SalesPerson, string>
    {
        public GetSalesPersonsFullNameSpecification()
        {
            Query.Select(s => s.FullName);
        }
    }
}
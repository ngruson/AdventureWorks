using Ardalis.Specification;

namespace AW.Application.Specifications
{
    public class GetSalesPersonsSpecification : Specification<Domain.Sales.SalesPerson>
    {
        public GetSalesPersonsSpecification(string territory) : base()
        {
            Query
                .Where(sp => sp.SalesTerritory.Name == territory);
        }
    }
}
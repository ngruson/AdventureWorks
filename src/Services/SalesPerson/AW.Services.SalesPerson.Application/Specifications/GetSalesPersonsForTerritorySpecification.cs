using Ardalis.Specification;

namespace AW.Services.SalesPerson.Application.Specifications
{
    public class GetSalesPersonsForTerritorySpecification : Specification<Domain.SalesPerson>
    {
        public GetSalesPersonsForTerritorySpecification(string territory) : base()
        {
            Query.Include(sp => sp.EmailAddresses);
            Query.Include(sp => sp.PhoneNumbers);

            Query
                .Where(sp => sp.Territory == territory);
        }
    }
}
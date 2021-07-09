using Ardalis.Specification;

namespace AW.Services.SalesPerson.Core.Specifications
{
    public class GetSalesPersonsForTerritorySpecification : Specification<Entities.SalesPerson>
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
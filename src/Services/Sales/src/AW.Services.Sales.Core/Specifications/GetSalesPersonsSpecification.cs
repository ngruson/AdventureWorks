using Ardalis.Specification;

namespace AW.Services.Sales.Core.Specifications
{
    public class GetSalesPersonsSpecification : Specification<Entities.SalesPerson>
    {
        public GetSalesPersonsSpecification() : base()
        {
            Query.Include(sp => sp.EmailAddresses);
            Query.Include(sp => sp.PhoneNumbers);
        }

        public GetSalesPersonsSpecification(string territory) : this()
        {
            Query.Where(sp => sp.Territory == territory);
        }
    }
}
using Ardalis.Specification;

namespace AW.Services.SalesPerson.Application.Specifications
{
    public class GetSalesPersonsSpecification : Specification<Domain.SalesPerson>
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
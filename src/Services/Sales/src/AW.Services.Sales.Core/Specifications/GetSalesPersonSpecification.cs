using Ardalis.Specification;

namespace AW.Services.Sales.Core.Specifications
{
    public class GetSalesPersonSpecification : Specification<Entities.SalesPerson>, ISingleResultSpecification
    {
        public GetSalesPersonSpecification(string firstName, string middleName, string lastName) : base()
        {
            Query.Include(sp => sp.EmailAddresses);
            Query.Include(sp => sp.PhoneNumbers);

            Query
                .Where(sp => sp.FirstName == firstName && sp.MiddleName == middleName && sp.LastName == lastName);
        }
    }
}
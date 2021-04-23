using Ardalis.Specification;

namespace AW.Services.SalesPerson.Application.Specifications
{
    public class GetSalesPersonSpecification : Specification<Domain.SalesPerson>, ISingleResultSpecification
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
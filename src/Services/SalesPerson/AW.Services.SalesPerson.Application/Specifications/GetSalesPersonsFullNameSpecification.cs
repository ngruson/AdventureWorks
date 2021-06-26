using Ardalis.Specification;
using AW.Common.Extensions;

namespace AW.Services.SalesPerson.Application.Specifications
{
    public class GetSalesPersonsFullNameSpecification : Specification<Domain.SalesPerson, string>
    {
        public GetSalesPersonsFullNameSpecification()
        {
            Query.Select(s => s.FullName());
        }
    }
}
using Ardalis.Specification;
using AW.SharedKernel.Extensions;

namespace AW.Services.SalesPerson.Core.Specifications
{
    public class GetSalesPersonsFullNameSpecification : Specification<Entities.SalesPerson, string>
    {
        public GetSalesPersonsFullNameSpecification()
        {
            Query.Select(s => s.FullName());
        }
    }
}
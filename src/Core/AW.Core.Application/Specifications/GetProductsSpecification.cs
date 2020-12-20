using Ardalis.Specification;

namespace AW.Core.Application.Specifications
{
    public class GetProductsSpecification : Specification<Domain.Production.Product>
    {
        public GetProductsSpecification() : base()
        {
            Query
                .Where(p => p.DiscontinuedDate == null && p.ListPrice > 0);
        }
    }
}
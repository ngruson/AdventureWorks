using Ardalis.Specification;
using AW.Domain.Production;

namespace AW.Application.Specifications
{
    public class GetProductsSpecification : Specification<Product>
    {
        public GetProductsSpecification() : base()
        {
            Query
                .Where(p => p.DiscontinuedDate == null && p.ListPrice > 0);
        }
    }
}
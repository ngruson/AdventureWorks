using Ardalis.Specification;
using AW.Domain.Production;

namespace AW.Application.Specifications
{
    public class ProductFilterSpecification : Specification<Product>
    {
        public ProductFilterSpecification() : base()
        {
            Query
                .Where(p => p.DiscontinuedDate == null && p.ListPrice > 0);
        }
    }
}
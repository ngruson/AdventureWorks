using Ardalis.Specification;

namespace AW.Services.Product.Core.Specifications
{
    public class GetProductsCountSpecification : Specification<Core.Entities.Product>
    {
        public GetProductsCountSpecification() : base()
        {
            Query
                .Where(p => p.DiscontinuedDate == null && p.ListPrice > 0);
        }
    }
}
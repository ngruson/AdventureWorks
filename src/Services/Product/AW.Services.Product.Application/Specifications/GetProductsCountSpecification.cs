using Ardalis.Specification;

namespace AW.Services.Product.Application.Specifications
{
    public class GetProductsCountSpecification : Specification<Domain.Product>
    {
        public GetProductsCountSpecification() : base()
        {
            Query
                .Where(p => p.DiscontinuedDate == null && p.ListPrice > 0);
        }
    }
}
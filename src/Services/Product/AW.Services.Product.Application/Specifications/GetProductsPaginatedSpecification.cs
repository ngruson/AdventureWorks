using Ardalis.Specification;

namespace AW.Services.Product.Application.Specifications
{
    public class GetProductsPaginatedSpecification : Specification<Domain.Product>
    {
        public GetProductsPaginatedSpecification(int pageIndex, int pageSize) : base()
        {
            Query
                .Where(p => p.DiscontinuedDate == null && p.ListPrice > 0)
                .Skip(pageIndex * pageSize)
                .Take(10)
                .OrderBy(p => p.Name);

            Query
                .Include(p => p.ProductSubcategory)
                    .ThenInclude(s => s.ProductCategory);
        }
    }
}
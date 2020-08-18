using Ardalis.Specification;
using AW.Domain.Production;

namespace AW.Application.Specifications
{
    public class ProductFilterPaginatedSpecification : Specification<Product>
    {
        public ProductFilterPaginatedSpecification(int pageIndex, int pageSize) : base()
        {
            Query
                .Where(p => p.DiscontinuedDate == null && p.ListPrice > 0)
                .Paginate(pageIndex * pageSize, pageSize)
                .OrderBy(p => p.Name);
        }
    }
}
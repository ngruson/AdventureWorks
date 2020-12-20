using Ardalis.Specification;

namespace AW.Core.Application.Specifications
{
    public class GetProductsPaginatedSpecification : Specification<Domain.Production.Product>
    {
        public GetProductsPaginatedSpecification(int pageIndex, int pageSize) : base()
        {
            Query
                .Where(p => p.DiscontinuedDate == null && p.ListPrice > 0)
                .Paginate(pageIndex * pageSize, pageSize)
                .OrderBy(p => p.Name);
        }
    }
}
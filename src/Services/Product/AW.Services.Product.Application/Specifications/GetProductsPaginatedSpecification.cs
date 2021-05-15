using Ardalis.Specification;
using AW.Services.Product.Application.Common;

namespace AW.Services.Product.Application.Specifications
{
    public class GetProductsPaginatedSpecification : Specification<Domain.Product>
    {
        public GetProductsPaginatedSpecification(
            int pageIndex, 
            int pageSize, 
            string category,
            string subcategory,
            OrderByClause<Domain.Product> orderByClause) : base()
        {
            Query
                .Include(p => p.ProductSubcategory)
                    .ThenInclude(s => s.ProductCategory);

            Query.Include(p => p.ProductProductPhotos)
                .ThenInclude(ppp => ppp.ProductPhoto);

            Query
                .Where(p => p.DiscontinuedDate == null && p.ListPrice > 0)
                .Skip(pageIndex * pageSize)
                .Take(pageSize);

            if (!string.IsNullOrEmpty(category))
                Query.Where(p => p.ProductSubcategory.ProductCategory.Name == category);

            if (!string.IsNullOrEmpty(subcategory))
                Query.Where(p => p.ProductSubcategory.Name == subcategory);

            if (orderByClause != null)
            {
                if (orderByClause.Direction == OrderByDirection.Ascending)
                    Query.OrderBy(orderByClause.Expression);
                else if (orderByClause.Direction == OrderByDirection.Descending)
                    Query.OrderByDescending(orderByClause.Expression);
            }
        }
    }
}
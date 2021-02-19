using Ardalis.Specification;

namespace AW.Services.Product.Application.Specifications
{
    public class GetProductCategoriesSpecification : Specification<Domain.ProductCategory>
    {
        public GetProductCategoriesSpecification() : base()
        {
            Query
                .Include(c => c.ProductSubcategory)
                .ThenInclude(s => s.Products);
        }
    }
}
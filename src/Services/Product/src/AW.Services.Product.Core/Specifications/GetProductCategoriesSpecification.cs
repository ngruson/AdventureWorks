using Ardalis.Specification;

namespace AW.Services.Product.Core.Specifications
{
    public class GetProductCategoriesSpecification : Specification<Core.Entities.ProductCategory>
    {
        public GetProductCategoriesSpecification() : base()
        {
            Query
                .Include(c => c.ProductSubcategory)
                .ThenInclude(s => s.Products);
        }
    }
}
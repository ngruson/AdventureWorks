using Ardalis.Specification;

namespace AW.Services.Product.Core.Specifications
{
    public class CountProductsSpecification : Specification<Core.Entities.Product>
    {
        public CountProductsSpecification(string category, string subcategory) : base()
        {
            Query
                .Where(c =>
                    (string.IsNullOrEmpty(category) || c.ProductSubcategory.ProductCategory.Name == category) &&
                    (string.IsNullOrEmpty(subcategory) || c.ProductSubcategory.Name == subcategory)
                );
        }
    }
}
using Ardalis.Specification;

namespace AW.Services.Product.Core.Specifications
{
    public class GetProductSubcategorySpecification : Specification<Entities.ProductSubcategory>, ISingleResultSpecification<Entities.ProductSubcategory>
    {
        public GetProductSubcategorySpecification(string name)
        {
            Query.Where(p => p.Name == name);
            Query.Include(_ => _.ProductCategory);
        }
    }
}

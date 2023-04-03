using Ardalis.Specification;

namespace AW.Services.Product.Core.Specifications
{
    public class ProductExistsSpecification : Specification<Entities.Product>
    {
        public ProductExistsSpecification(string productNumber) : base()
        {
            Query
                .Where(p => p.ProductNumber == productNumber);
        }
    }
}

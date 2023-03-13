using Ardalis.Specification;

namespace AW.Services.Product.Core.Specifications
{
    public class GetProductSpecification : Specification<Entities.Product>, ISingleResultSpecification<Entities.Product>
    {
        public GetProductSpecification(string? productNumber) : base()
        {
            Query
                .Where(p => p.ProductNumber == productNumber)
                .Include(p => p.ProductProductPhotos)
                    .ThenInclude(p => p.ProductPhoto);

            Query
                .Include(p => p.ProductSubcategory)
                .ThenInclude(ps => ps!.ProductCategory);

            Query
                .Include(_ => _.ProductModel);
        }
    }
}

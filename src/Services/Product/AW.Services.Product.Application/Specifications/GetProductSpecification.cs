using Ardalis.Specification;

namespace AW.Services.Product.Application.Specifications
{
    public class GetProductSpecification : Specification<Domain.Product>
    {
        public GetProductSpecification(string productNumber) : base()
        {
            Query
                .Where(p => p.ProductNumber == productNumber)
                .Include(p => p.ProductProductPhotos)
                    .ThenInclude(p => p.ProductPhoto);

            Query
                .Include(p => p.ProductSubcategory)
                .ThenInclude(ps => ps.ProductCategory);
        }
    }
}
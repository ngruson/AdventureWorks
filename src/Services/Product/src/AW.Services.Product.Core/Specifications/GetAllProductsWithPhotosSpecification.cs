using Ardalis.Specification;

namespace AW.Services.Product.Core.Specifications
{
    public class GetAllProductsWithPhotosSpecification : Specification<Entities.Product>
    {
        public GetAllProductsWithPhotosSpecification()
        {
            Query.Include(p => p.ProductProductPhotos)
                .ThenInclude(ppp => ppp.ProductPhoto);
        }
    }
}
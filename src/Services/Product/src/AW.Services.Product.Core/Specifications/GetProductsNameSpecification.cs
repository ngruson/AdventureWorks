using Ardalis.Specification;

namespace AW.Services.Product.Core.Specifications
{
    public class GetProductsNameSpecification : Specification<Entities.Product, string>
    {

        public GetProductsNameSpecification()
        {
            Query!.Select(p => p.Name);
        }
    }
}
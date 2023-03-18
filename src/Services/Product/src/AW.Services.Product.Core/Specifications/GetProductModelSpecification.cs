using Ardalis.Specification;

namespace AW.Services.Product.Core.Specifications
{
    public class GetProductModelSpecification : Specification<Entities.ProductModel>, ISingleResultSpecification<Entities.ProductModel>
    {
        public GetProductModelSpecification(string name)
        {
            Query.Where(p => p.Name == name);
        }
    }
}

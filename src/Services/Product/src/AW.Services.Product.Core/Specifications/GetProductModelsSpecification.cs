using Ardalis.Specification;

namespace AW.Services.Product.Core.Specifications
{
    public class GetProductModelsSpecification : Specification<Entities.ProductModel>
    {
        public GetProductModelsSpecification()
        {
            Query.OrderBy(_ => _.Name);
        }
    }
}

using Ardalis.Specification;

namespace AW.Services.Product.Core.Specifications
{
    public class GetProductByProductNumberSpecification : Specification<Entities.Product>, ISingleResultSpecification<Entities.Product>
    {
        public GetProductByProductNumberSpecification(string productNumber)
        {
            Query
                .Where(p => p.ProductNumber == productNumber);
        }
    }
}
using Ardalis.Specification;

namespace AW.Services.Product.Persistence.EF.UnitTests.Specifications
{
    public class GetProductByProductNumberSpecification : Specification<Domain.Product>, ISingleResultSpecification
    {
        public GetProductByProductNumberSpecification(string productNumber)
        {
            Query
                .Where(p => p.ProductNumber == productNumber);
        }
    }
}
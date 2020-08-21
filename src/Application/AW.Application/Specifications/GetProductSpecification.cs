using Ardalis.Specification;
using AW.Domain.Production;

namespace AW.Application.Specifications
{
    public class GetProductSpecification : Specification<Product>
    {
        public GetProductSpecification(string productNumber) : base()
        {
            Query
                .Where(p => p.ProductNumber == productNumber);
        }
    }
}
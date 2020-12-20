using Ardalis.Specification;

namespace AW.Core.Application.Specifications
{
    public class GetProductSpecification : Specification<Domain.Production.Product>
    {
        public GetProductSpecification(string productNumber) : base()
        {
            Query
                .Where(p => p.ProductNumber == productNumber);
        }
    }
}
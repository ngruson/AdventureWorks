using Ardalis.Specification;
using System.Linq;

namespace AW.Services.Product.Persistence.EF.UnitTests.Specifications
{
    public class GetProductsByColorSpecification : Specification<Domain.Product>
    {
        public GetProductsByColorSpecification(string color)
        {
            Query.Where(p => p.Color == color);
        }
    }
}
using Ardalis.Specification;
using System.Linq;

namespace AW.Services.Product.Core.Specifications
{
    public class GetProductsByColorSpecification : Specification<Entities.Product>
    {
        public GetProductsByColorSpecification(string color)
        {
            Query.Where(p => p.Color == color);
        }
    }
}
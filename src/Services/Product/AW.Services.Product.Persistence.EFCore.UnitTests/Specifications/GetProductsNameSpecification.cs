using Ardalis.Specification;

namespace AW.Services.Product.Persistence.EFCore.UnitTests.Specifications
{
    public class GetProductsNameSpecification : Specification<Domain.Product, string>
    {

        public GetProductsNameSpecification()
        {
            Query.Select(p => p.Name);
        }
    }
}
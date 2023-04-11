using Ardalis.Specification;

namespace AW.Services.Product.Core.Specifications
{
    public class GetLocationsSpecification : Specification<Entities.Location>
    {
        public GetLocationsSpecification()
        {
            Query.OrderBy(_ => _.Id);
        }
    }
}

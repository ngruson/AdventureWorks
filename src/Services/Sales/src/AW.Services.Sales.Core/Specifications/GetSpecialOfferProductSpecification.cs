using Ardalis.Specification;
using AW.Services.Sales.Core.Entities;

namespace AW.Services.Sales.Core.Specifications
{
    public class GetSpecialOfferProductSpecification : Specification<SpecialOfferProduct>
    {
        public GetSpecialOfferProductSpecification(string productNumber, string? description = null)
        {
            Query.Include(_ => _.SpecialOffer);
            Query
                .Where(s => s.ProductNumber == productNumber);

            if (!string.IsNullOrEmpty(description))
            {
                Query
                    .Where(_ => _.SpecialOffer!.Description == description);
            }
        }
    }
}
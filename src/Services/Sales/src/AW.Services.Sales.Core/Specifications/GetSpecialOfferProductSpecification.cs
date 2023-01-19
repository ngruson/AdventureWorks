using Ardalis.Specification;
using AW.Services.Sales.Core.Entities;

namespace AW.Services.Sales.Core.Specifications
{
    public class GetSpecialOfferProductSpecification : Specification<SpecialOfferProduct>
    {
        public GetSpecialOfferProductSpecification(string productNumber)
        {
            Query.Include(_ => _.SpecialOffer);
            Query
                .Where(s => s.ProductNumber == productNumber);
        }
    }
}
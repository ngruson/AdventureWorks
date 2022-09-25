using Ardalis.Specification;
using AW.Services.Sales.Core.Entities;

namespace AW.Services.Sales.Core.Specifications
{
    public class GetSpecialOfferProductSpecification : Specification<SpecialOfferProduct>, ISingleResultSpecification<SpecialOfferProduct>
    {
        public GetSpecialOfferProductSpecification(string productNumber)
        {
            Query
                .Where(s => s.ProductNumber == productNumber);
        }
    }
}
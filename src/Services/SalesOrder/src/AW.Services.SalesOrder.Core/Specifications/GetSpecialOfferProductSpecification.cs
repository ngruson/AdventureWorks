using Ardalis.Specification;
using AW.Services.SalesOrder.Core.Entities;

namespace AW.Services.SalesOrder.Core.Specifications
{
    public class GetSpecialOfferProductSpecification : Specification<SpecialOfferProduct>, ISingleResultSpecification
    {
        public GetSpecialOfferProductSpecification(string productNumber)
        {
            Query
                .Where(s => s.ProductNumber == productNumber);
        }
    }
}
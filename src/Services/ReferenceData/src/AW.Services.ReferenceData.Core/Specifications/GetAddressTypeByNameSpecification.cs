using Ardalis.Specification;
using AW.Services.ReferenceData.Core.Entities;

namespace AW.Services.ReferenceData.Core.Specifications
{
    public class GetAddressTypeByNameSpecification : Specification<AddressType>, ISingleResultSpecification<AddressType>
    {
        public GetAddressTypeByNameSpecification(string name)
        {
            Query
                .Where(p => p.Name == name);

            Query.OrderBy(_ => _.Name);
        }
    }
}
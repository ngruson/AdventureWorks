using Ardalis.Specification;
using AW.Services.ReferenceData.Core.Entities;

namespace AW.Services.ReferenceData.Core.Specifications
{
    public class GetAddressTypesNameSpecification : Specification<AddressType, string>
    {

        public GetAddressTypesNameSpecification()
        {
            Query.Select(p => p.Name);
        }
    }
}
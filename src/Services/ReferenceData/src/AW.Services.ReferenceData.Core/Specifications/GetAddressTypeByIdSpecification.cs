using Ardalis.Specification;
using AW.Services.ReferenceData.Core.Entities;

namespace AW.Services.ReferenceData.Core.Specifications
{
    public class GetAddressTypeByIdSpecification : Specification<AddressType>, ISingleResultSpecification
    {
        public GetAddressTypeByIdSpecification(int id)
        {
            Query
                .Where(p => p.Id == id);
        }
    }
}
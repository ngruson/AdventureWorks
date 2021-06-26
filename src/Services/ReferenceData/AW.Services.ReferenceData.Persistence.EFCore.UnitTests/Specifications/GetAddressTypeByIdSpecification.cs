using Ardalis.Specification;
using AW.Services.ReferenceData.Domain;

namespace AW.Services.ReferenceData.Persistence.EF.UnitTests.Specifications
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
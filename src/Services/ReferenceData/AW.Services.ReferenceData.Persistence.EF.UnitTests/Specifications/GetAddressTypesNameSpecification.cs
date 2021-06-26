using Ardalis.Specification;
using AW.Services.ReferenceData.Domain;

namespace AW.Services.ReferenceData.Persistence.EF.UnitTests.Specifications
{
    public class GetAddressTypesNameSpecification : Specification<AddressType, string>
    {

        public GetAddressTypesNameSpecification()
        {
            Query.Select(p => p.Name);
        }
    }
}
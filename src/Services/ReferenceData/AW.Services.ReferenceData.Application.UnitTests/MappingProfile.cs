using AW.Common.AutoMapper;
using AW.Services.ReferenceData.Application.AddressType.GetAddressTypes;

namespace AW.Services.ReferenceData.Application.UnitTests
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(typeof(GetAddressTypesQuery).Assembly);
        }
    }
}
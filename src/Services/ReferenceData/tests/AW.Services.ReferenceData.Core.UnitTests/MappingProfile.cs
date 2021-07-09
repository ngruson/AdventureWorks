using AW.Services.ReferenceData.Core.Handlers.AddressType.GetAddressTypes;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.ReferenceData.Core.UnitTests
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(typeof(GetAddressTypesQuery).Assembly);
        }
    }
}
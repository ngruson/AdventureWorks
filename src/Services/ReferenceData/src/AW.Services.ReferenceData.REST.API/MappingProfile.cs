using AW.Services.ReferenceData.Core.Handlers.AddressType.GetAddressTypes;
using AW.SharedKernel.AutoMapper;
using System.Reflection;

namespace AW.Services.ReferenceData.REST.API
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
            ApplyMappingsFromAssembly(typeof(GetAddressTypesQuery).Assembly);
        }
    }
}
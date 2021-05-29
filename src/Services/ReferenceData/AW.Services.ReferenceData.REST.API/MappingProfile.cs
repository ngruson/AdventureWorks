using AW.Common.AutoMapper;
using AW.Services.ReferenceData.Application.AddressType.GetAddressTypes;
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
using AW.Services.CountryRegion.Application.Common;
using System.Reflection;

namespace AW.Services.CountryRegion.REST.API
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
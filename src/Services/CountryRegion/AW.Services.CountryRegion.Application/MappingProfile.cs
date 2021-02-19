using AW.Services.CountryRegion.Application.Common;
using System.Reflection;

namespace AW.Services.CountryRegion.Application
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
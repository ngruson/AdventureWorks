using AW.Services.ReferenceData.Application.Common;
using System.Reflection;

namespace AW.Services.ReferenceData.REST.API
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
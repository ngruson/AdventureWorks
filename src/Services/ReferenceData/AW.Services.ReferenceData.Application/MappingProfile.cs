using AW.Common.AutoMapper;
using System.Reflection;

namespace AW.Services.ReferenceData.Application
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
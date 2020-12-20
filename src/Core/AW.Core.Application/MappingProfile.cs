using AW.Core.Application.AutoMapper;
using System.Reflection;

namespace AW.Core.Application
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
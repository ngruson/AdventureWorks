using AW.Application.AutoMapper;
using System.Reflection;

namespace AW.Services.API.CustomerAPI
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
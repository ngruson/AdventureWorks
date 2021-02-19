using AW.Core.Application.AutoMapper;
using System.Reflection;

namespace AW.Services.REST.CustomerAPI
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
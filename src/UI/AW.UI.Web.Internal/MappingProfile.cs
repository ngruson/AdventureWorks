using AW.Application.AutoMapper;
using System.Reflection;

namespace AW.UI.Web.Internal
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
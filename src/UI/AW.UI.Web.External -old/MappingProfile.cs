using AW.Core.Application.AutoMapper;
using System.Reflection;

namespace AW.UI.Web.External
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}